using BankSystem.Api.Dto.ClientDto;
using BankSystem.Api.FluentValidations.ClientValidations;

namespace BankSystem.Application.FluentValidations.ClientValidations
{
    public class CreateClientRequestValidator : BaseClientRequestValidator<CreateClientRequest>
    {
        public CreateClientRequestValidator()
        {
            ApplyCommonRules();
        }
    }
}
