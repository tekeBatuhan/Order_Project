using Business.BusinessAspects;
using Business.Handlers.Colors.Queries;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Dtos;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.WareHouses.Queries
{
    public class GetWareHousesLookUpQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
    {
        public class GetWareHousesLookUpQueryHandler : IRequestHandler<GetWareHousesLookUpQuery,
               IDataResult<IEnumerable<SelectionItem>>>
        {
            private readonly IWareHouseRepository _wareHouseRepository;
            private readonly IMediator _mediator;

            public GetWareHousesLookUpQueryHandler(IWareHouseRepository wareHouseRepository, IMediator mediator)
            {
                _wareHouseRepository = wareHouseRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetWareHousesLookUpQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<SelectionItem>>(
                                  await _wareHouseRepository.GetWareHousesLookUp());
            }
        }
    }
}
