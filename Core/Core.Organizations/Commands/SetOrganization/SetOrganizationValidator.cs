using Core.Organizations.CustomValidators;
using Data.Abstractions;
using FluentValidation;
using System;
using System.Linq;

namespace Core.Organizations.Commands.SetOrganization
{
    public class SetOrganizationValidator : AbstractValidator<SetOrganizationCommand>
    {
        public SetOrganizationValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Идентификатор пользователя должен быть больше нуля");
            RuleFor(x => x.OrganizationId).SetValidator(new OrganizationExistsValidator(unitOfWork));
        }
    }
}
