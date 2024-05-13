
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.WareHouseProductMappings.Queries
{

    public class GetWareHouseProductMappingsQuery : IRequest<IDataResult<IEnumerable<WareHouseProductMapping>>>
    {
        public class GetWareHouseProductMappingsQueryHandler : IRequestHandler<GetWareHouseProductMappingsQuery, IDataResult<IEnumerable<WareHouseProductMapping>>>
        {
            private readonly IWareHouseProductMappingRepository _wareHouseProductMappingRepository;
            private readonly IMediator _mediator;

            public GetWareHouseProductMappingsQueryHandler(IWareHouseProductMappingRepository wareHouseProductMappingRepository, IMediator mediator)
            {
                _wareHouseProductMappingRepository = wareHouseProductMappingRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<WareHouseProductMapping>>> Handle(GetWareHouseProductMappingsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<WareHouseProductMapping>>(await _wareHouseProductMappingRepository.GetListAsync());
            }
        }
    }
}