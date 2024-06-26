﻿
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
using Business.Handlers.Products.ValidationRules;
using Entities.Enums;
using System;

namespace Business.Handlers.Products.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateProductCommand : IRequest<IResult>
    {

        public int CreatedUserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
        public bool isDeleted { get; set; }
        public string Name { get; set; }
        public int ColorId { get; set; }
        public Size Size { get; set; }
        public System.Collections.Generic.List<WareHouseProductMapping> WareHouseProductMappings { get; set; }
        public System.Collections.Generic.List<Order> Orders { get; set; }


        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, IResult>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMediator _mediator;
            public CreateProductCommandHandler(IProductRepository productRepository, IMediator mediator)
            {
                _productRepository = productRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateProductValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var isThereProductRecord = _productRepository.Query().Any(u => u.Name == request.Name && u.Status);

                if (isThereProductRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedProduct = new Product
                {
                    CreatedUserId = request.CreatedUserId,
                    CreatedDate = request.CreatedDate,
                    LastUpdatedUserId = request.LastUpdatedUserId,
                    LastUpdatedDate = request.LastUpdatedDate,
                    Status = request.Status,
                    isDeleted = false,
                    Name = request.Name,
                    ColorId = request.ColorId,
                    WareHouseProductMappings = request.WareHouseProductMappings,
                    Orders = request.Orders,
                    Size = (Size)Enum.Parse(typeof(Size), request.Size.ToString()),

                };

                var result = _productRepository.Add(addedProduct);
                await _productRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added, result.Id);
            }
        }
    }
}