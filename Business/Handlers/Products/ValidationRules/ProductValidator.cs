﻿
using Business.Handlers.Products.Commands;
using FluentValidation;

namespace Business.Handlers.Products.ValidationRules
{

    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.ColorId).NotEmpty();
            //RuleFor(x => x.WareHouseProductMappings).NotEmpty();
            //RuleFor(x => x.Orders).NotEmpty();

        }
    }
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.ColorId).NotEmpty();
            //RuleFor(x => x.WareHouseProductMappings).NotEmpty();
            //RuleFor(x => x.Orders).NotEmpty();

        }
    }
}