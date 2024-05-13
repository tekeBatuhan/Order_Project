
using Business.Handlers.WareHouses.Commands;
using FluentValidation;

namespace Business.Handlers.WareHouses.ValidationRules
{

    public class CreateWareHouseValidator : AbstractValidator<CreateWareHouseCommand>
    {
        public CreateWareHouseValidator()
        {
            RuleFor(x => x.WareHouseProductMappings).NotEmpty();

        }
    }
    public class UpdateWareHouseValidator : AbstractValidator<UpdateWareHouseCommand>
    {
        public UpdateWareHouseValidator()
        {
            RuleFor(x => x.WareHouseProductMappings).NotEmpty();

        }
    }
}