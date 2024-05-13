
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
                var isThereWareHouseProductMappingRecord = _wareHouseProductMappingRepository.Query().Any(u => u.CreatedUserId == request.CreatedUserId);

                if (isThereWareHouseProductMappingRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedWareHouseProductMapping = new WareHouseProductMapping
                {
                    CreatedUserId = request.CreatedUserId,
                    CreatedDate = request.CreatedDate,
                    LastUpdatedUserId = request.LastUpdatedUserId,
                    LastUpdatedDate = request.LastUpdatedDate,
                    Status = request.Status,
                    isDeleted = request.isDeleted,
                    ProductId = request.ProductId,
                    WareHouseId = request.WareHouseId,

                };

                _wareHouseProductMappingRepository.Add(addedWareHouseProductMapping);
                await _wareHouseProductMappingRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}