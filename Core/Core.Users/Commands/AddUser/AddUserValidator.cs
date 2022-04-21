using FluentValidation;
using System.Text.RegularExpressions;

namespace Core.Users.Commands.AddUser
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        private const string PHONE_REGEX = @"(\+7|8|\b)[\(\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[)\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)";
        public AddUserValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Name).NotEmpty().WithMessage("Имя не должно быть пустым");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Фамилия не должна быть пустой");
            RuleFor(x => x.Patronymic).NotEmpty().WithMessage("Отчество не должно быть пустым");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Некорректный формат электронной почты");
            RuleFor(x => x.Phone).Must(phone => Regex.IsMatch(phone, PHONE_REGEX)).WithMessage("Некорректный формат номера телефона");
        }
    }
}
