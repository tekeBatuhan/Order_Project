
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.WareHouseProductMappings.ValidationRules;


namespace Business.Handlers.WareHouseProductMappings.Commands
{


    public class UpdateWareHouseProductMappingCommand : IRequest<IResult>
    {
        public int Id { get; set; }
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

        public class UpdateWareHouseProductMappingCommandHandler : IRequestHandler<UpdateWareHouseProductMappingCommand, IResult>
        {
            private readonly IWareHouseProductMappingRepository _wareHouseProductMappingRepository;
            private readonly IMediator _mediator;

            public UpdateWareHouseProductMappingCommandHandler(IWareHouseProductMappingRepository wareHouseProductMappingRepository, IMediator mediator)
            {
                _wareHouseProductMappingRepository = wareHouseProductMappingRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateWareHouseProductMappingValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateWareHouseProductMappingCommand request, CancellationToken cancellationToken)
            {
                var isThereWareHouseProductMappingRecord = await _wareHouseProductMappingRepository.GetAsync(u => u.Id == request.Id);

                isThereWareHouseProductMappingRecord.LastUpdatedUserId = request.LastUpdatedUserId;
                isThereWareHouseProductMappingRecord.LastUpdatedDate = request.LastUpdatedDate;
                isThereWareHouseProductMappingRecord.Status = request.Status;
                isThereWareHouseProductMappingRecord.isDeleted = request.isDeleted;
                isThereWareHouseProductMappingRecord.ProductId = request.ProductId;
                isThereWareHouseProductMappingRecord.WareHouseId = request.WareHouseId;
                isThereWareHouseProductMappingRecord.ReadyForSale = request.ReadyForSale;
                isThereWareHouseProductMappingRecord.Count = request.Count;

                _wareHouseProductMappingRepository.Update(isThereWareHouseProductMappingRecord);
                await _wareHouseProductMappingRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

