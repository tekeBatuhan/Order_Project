
using Business.Handlers.WareHouseProductMappings.Commands;
using FluentValidation;

namespace Business.Handlers.WareHouseProductMappings.ValidationRules
{

    public class CreateWareHouseProductMappingValidator : AbstractValidator<CreateWareHouseProductMappingCommand>
    {
        public CreateWareHouseProductMappingValidator()
        {
            RuleFor(x => x.WareHouseId).NotEmpty();

        }
    }
    public class UpdateWareHouseProductMappingValidator : AbstractValidator<UpdateWareHouseProductMappingCommand>
    {
        public UpdateWareHouseProductMappingValidator()
        {
            RuleFor(x => x.WareHouseId).NotEmpty();

        }
    }
}