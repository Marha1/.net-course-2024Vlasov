using BankSystem.Api.Dto.ClientDto;
using FluentValidation;

namespace BankSystem.Api.FluentValidations.ClientValidations
{
    public class UpdateClientRequestValidator : BaseClientRequestValidator<UpdateClientRequest>
    {
        public UpdateClientRequestValidator()
        {
            ApplyCommonRules();

            RuleFor(x => x.ClientId)
                .NotEmpty()
                .WithMessage("Требуется ClientId.");
        }
    }
}
