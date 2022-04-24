using Core.Users.Commands.AddUser;
using EventBus;
using MassTransit;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace Core.Users.Tests
{
    public class UsersTests
    {
        public UsersTests()
        {
        }

        [Fact]
        public async void AddUserCommand_NoException()
        {
            // Arrange
            var logger = new Mock<ILogger<AddUserCommandHandler>>();
            var bus = new Mock<IEventBus>();

            var command = new AddUserCommand()
            {
                Name = "Adam",
                Surname = "Adamson",
                Patronymic = "Alex",
                Email = "test@test.com",
                Phone = "89111111111"
            };
            var handler = new AddUserCommandHandler(logger.Object, bus.Object);

            // Act
            var exception = await Record.ExceptionAsync(() => handler.Handle(command));

            // Assert
            Assert.Null(exception);
        }
    }
}
