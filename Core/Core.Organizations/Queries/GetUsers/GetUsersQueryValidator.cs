using Core.Organizations.CustomValidators;
using Data.UnitOfWork;
using FluentValidation;

namespace Core.Organizations.Queries.GetUsers
{
    public class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
    {
        public GetUsersQueryValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.Page).NotEmpty().WithMessage("Номер страницы должен принимать значение больше нуля");
            RuleFor(x => x.RowsPerPage).NotEmpty().WithMessage("Необходимо указать количество получаемых элементов");
            RuleFor(x => x.OrganizationId).SetValidator(new OrganizationExistsValidator(unitOfWork));
        }
    }
}
