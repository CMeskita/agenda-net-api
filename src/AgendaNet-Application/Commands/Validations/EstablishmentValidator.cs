using AgendaNet_Domain.Entities;
using FluentValidation;
namespace AgendaNet_Application.Commands.Validations
{
    class EstablishmentValidator : AbstractValidator<Establishment>
    {
        public EstablishmentValidator() 
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome do estabelecimento é obrigatório.")
            .MaximumLength(100).WithMessage("O nome não pode ter mais que 100 caracteres.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("A descrição é obrigatória.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("O endereço é obrigatório.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("O telefone é obrigatório.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("Formato de e-mail inválido.");

            RuleFor(x => x.ThemeColor)
                .MaximumLength(20).WithMessage("A cor do tema deve ter no máximo 20 caracteres.");
        }
    }
}
