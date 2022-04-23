using Core.Organizations.Commands.AddUser;
using Data.Context;
using Data.UnitOfWork;
using MediatR;
using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.Extensions.Logging;

namespace Core.Organizations.Test
{
    public class AddUserCommandTests : DbTestsBase
    {
        public AddUserCommandTests() : base()
        {
        }

        [Fact]
        public async void AddUserCommand_DataUpdated()
        {
            // Arrange
            var logger = new Mock<ILogger<AddUserCommandHandler>>();

            var command = new AddUserCommand()
            {
                Name = "Adam",
                Surname = "Adamson",
                Patronymic = "Alex",
                Email = "test3@test3.com",
                Phone = "89111111111"
            };
            var handler = new AddUserCommandHandler(logger.Object, unitOfWork);

            // Act
            var exception = await Record.ExceptionAsync(() => handler.Handle(command));

            var createdUser = await context.Users.FirstOrDefaultAsync(x => string.Equals(x.Email, command.Email, StringComparison.InvariantCultureIgnoreCase));

            // Assert
            Assert.Null(exception);
            Assert.NotNull(createdUser);
        }
    }
}
