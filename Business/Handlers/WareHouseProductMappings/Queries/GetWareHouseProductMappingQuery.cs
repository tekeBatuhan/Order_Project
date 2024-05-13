
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.WareHouseProductMappings.Queries
{
    public class GetWareHouseProductMappingQuery : IRequest<IDataResult<WareHouseProductMapping>>
    {
        public int Id { get; set; }

        public class GetWareHouseProductMappingQueryHandler : IRequestHandler<GetWareHouseProductMappingQuery, IDataResult<WareHouseProductMapping>>
        {
            private readonly IWareHouseProductMappingRepository _wareHouseProductMappingRepository;
            private readonly IMediator _mediator;

            public GetWareHouseProductMappingQueryHandler(IWareHouseProductMappingRepository wareHouseProductMappingRepository, IMediator mediator)
            {
                _wareHouseProductMappingRepository = wareHouseProductMappingRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<WareHouseProductMapping>> Handle(GetWareHouseProductMappingQuery request, CancellationToken cancellationToken)
            {
                var wareHouseProductMapping = await _wareHouseProductMappingRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<WareHouseProductMapping>(wareHouseProductMapping);
            }
        }
    }
}
