using Business.BusinessAspects;
using Business.Handlers.Languages.Queries;
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

namespace Business.Handlers.Colors.Queries
{
    public class GetColorsLookUpQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
    {

        public class GetColorsLookUpQueryHandler : IRequestHandler<GetColorsLookUpQuery,
                IDataResult<IEnumerable<SelectionItem>>>
        {
            private readonly IColorRepository _colorRepository;
            private readonly IMediator _mediator;

            public GetColorsLookUpQueryHandler(IColorRepository colorRepository, IMediator mediator)
            {
                _colorRepository = colorRepository;
                _mediator = mediator;
            }

            [SecuredOperation(Priority = 1)]
            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]         
            public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetColorsLookUpQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<SelectionItem>>(
                                  await _colorRepository.GetColorsLookUp());
            }
        }
    }
}
