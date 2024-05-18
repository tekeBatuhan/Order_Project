
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
using System;


namespace Business.Handlers.Colors.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteColorCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public class DeleteColorCommandHandler : IRequestHandler<DeleteColorCommand, IResult>
        {
            private readonly IColorRepository _colorRepository;
            private readonly IMediator _mediator;

            public DeleteColorCommandHandler(IColorRepository colorRepository, IMediator mediator)
            {
                _colorRepository = colorRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteColorCommand request, CancellationToken cancellationToken)
            {
                var colorToDelete = _colorRepository.Get(p => p.Id == request.Id);
                colorToDelete.Status = false;
                colorToDelete.isDeleted = true;
                colorToDelete.LastUpdatedDate = DateTime.Now;
                colorToDelete.LastUpdatedUserId = request.UserId;

                _colorRepository.Update(colorToDelete);
                await _colorRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

