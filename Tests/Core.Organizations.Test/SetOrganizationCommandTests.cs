using Core.Organizations.Commands.SetOrganization;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Core.Organizations.Test
{
    public class SetOrganizationCommandTests : DbTestsBase
    {
        public SetOrganizationCommandTests() : base()
        {
            context.Users.Add(new User
            {
                Name = "Adam",
                Surname = "Wilson",
                Patronymic = "Daniel",
                Email = "test@test1.ru",
                Phone = "89000000000"
            });

            context.Organizations.Add(new Organization
            {
                Name = "Test organization"
            });

            context.SaveChanges();
        }

        [Fact]
        public async void SetOrganizationsCommand_DataUpdated()
        {
            // Arrange
            var logger = new Mock<ILogger<SetOrganizationCommandHandler>>();

            var user = await context.Users.FirstOrDefaultAsync();
            var organization = await context.Organizations.FirstOrDefaultAsync();

            var command = new SetOrganizationCommand()
            {
                UserId = user.Id,
                OrganizationId = organization.Id
            };

            var handler = new SetOrganizationCommandHandler(logger.Object, unitOfWork);

            // Act
            var exception = await Record.ExceptionAsync(() => handler.Handle(command));

            // Assert
            Assert.Null(exception);

            var updatedUser = await context.Users.FirstOrDefaultAsync();
            Assert.NotNull(updatedUser.OrganizationId);
        }
    }
}
