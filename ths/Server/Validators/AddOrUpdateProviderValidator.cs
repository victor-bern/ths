using FluentValidation;
using ths.Server.Models;

namespace ths.Server.Validators
{
    public class AddOrUpdateProviderValidator : AbstractValidator<AddOrUpdateProviderDto>
    {
        public AddOrUpdateProviderValidator()
        {
            RuleFor(x => x.CorporateName)
                .NotEmpty();

            RuleFor(x => x.Phone)
                .NotEmpty();

            RuleFor(x => x.Address)
                .NotEmpty();

            RuleFor(x => x.Document)
                .NotEmpty();
        }
    }
}
