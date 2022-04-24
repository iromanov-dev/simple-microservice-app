using Data.Abstractions;
using Data.Models;
using FluentValidation;
using System.Linq;

namespace Core.Organizations.CustomValidators
{
    public class OrganizationExistsValidator : AbstractValidator<long>
    {
        public OrganizationExistsValidator(IUnitOfWork unitOfWork) {
            RuleFor(id => id)
                .Custom((id, context) =>
                {
                    if (!unitOfWork.Repository<Organization>().GetAll().Any(o => o.Id == id))
                    {
                        context.AddFailure("Указанной организации не существует.");
                    }
                });
        }
    }
}
