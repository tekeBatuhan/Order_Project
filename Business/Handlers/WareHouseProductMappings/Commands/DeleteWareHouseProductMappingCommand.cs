
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.WareHouseProductMappings.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteWareHouseProductMappingCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteWareHouseProductMappingCommandHandler : IRequestHandler<DeleteWareHouseProductMappingCommand, IResult>
        {
            private readonly IWareHouseProductMappingRepository _wareHouseProductMappingRepository;
            private readonly IMediator _mediator;

            public DeleteWareHouseProductMappingCommandHandler(IWareHouseProductMappingRepository wareHouseProductMappingRepository, IMediator mediator)
            {
                _wareHouseProductMappingRepository = wareHouseProductMappingRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteWareHouseProductMappingCommand request, CancellationToken cancellationToken)
            {
                var wareHouseProductMappingToDelete = _wareHouseProductMappingRepository.Get(p => p.Id == request.Id);

                _wareHouseProductMappingRepository.Delete(wareHouseProductMappingToDelete);
                await _wareHouseProductMappingRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

