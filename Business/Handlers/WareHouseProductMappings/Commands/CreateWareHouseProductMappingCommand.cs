
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.WareHouseProductMappings.ValidationRules;

namespace Business.Handlers.WareHouseProductMappings.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateWareHouseProductMappingCommand : IRequest<IResult>
    {

        public int CreatedUserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool isDeleted { get; set; }
        public int ProductId { get; set; }
        public int WareHouseId { get; set; }
        public int Count { get; set; }
        public bool ReadyForSale { get; set; }


        public class CreateWareHouseProductMappingCommandHandler : IRequestHandler<CreateWareHouseProductMappingCommand, IResult>
        {
            private readonly IWareHouseProductMappingRepository _wareHouseProductMappingRepository;
            private readonly IMediator _mediator;
            public CreateWareHouseProductMappingCommandHandler(IWareHouseProductMappingRepository wareHouseProductMappingRepository, IMediator mediator)
            {
                _wareHouseProductMappingRepository = wareHouseProductMappingRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateWareHouseProductMappingValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateWareHouseProductMappingCommand request, CancellationToken cancellationToken)
            {
                var isThereWareHouseProductMappingRecord = _wareHouseProductMappingRepository.Query().Any(u => u.ProductId == request.ProductId && u.WareHouseId == request.WareHouseId && u.Status);

                if (isThereWareHouseProductMappingRecord == true)
                {
                    var mapping = _wareHouseProductMappingRepository.Query().FirstOrDefault(u => u.ProductId == request.ProductId && u.WareHouseId == request.WareHouseId && u.Status);
                    mapping.Count += request.Count;
                    _wareHouseProductMappingRepository.Update(mapping);
                }
                else 
                {
                    var addedWareHouseProductMapping = new WareHouseProductMapping
                    {
                        CreatedUserId = request.CreatedUserId,
                        CreatedDate = request.CreatedDate,
                        LastUpdatedUserId = request.LastUpdatedUserId,
                        LastUpdatedDate = request.LastUpdatedDate,
                        Status = request.Status,
                        isDeleted = false,
                        ProductId = request.ProductId,
                        WareHouseId = request.WareHouseId,
                        Count = request.Count,
                        ReadyForSale = request.ReadyForSale,

                    };

                    _wareHouseProductMappingRepository.Add(addedWareHouseProductMapping);

                }
                
                await _wareHouseProductMappingRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}