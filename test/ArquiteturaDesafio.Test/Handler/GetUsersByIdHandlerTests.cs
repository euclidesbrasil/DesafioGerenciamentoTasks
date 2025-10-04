using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using AutoMapper;
using ArquiteturaDesafio.Core.Application.UseCases.Queries.GetUsersById;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;
using NSubstitute;

namespace ArquiteturaDesafio.Tests.Application.Handlers;
public class GetUsersByIdHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnMappedResponse_WhenUserExists()
    {
        // Arrange
        var tokenService = Substitute.For<IJwtTokenService>();

        var userId = Guid.NewGuid();
        var user = new Entities.User("email@email.com", "userNmae", "senha", "Euclides", "Brasil",
            new ArquiteturaDesafio.Core.Domain.ValueObjects.Address("Fortaleza", "Rua", 1, "60000000",
            new ArquiteturaDesafio.Core.Domain.ValueObjects.Geolocation("1", "1")), "85999999999",
            ArquiteturaDesafio.Core.Domain.Enum.UserStatus.Active,
            ArquiteturaDesafio.Core.Domain.Enum.UserRole.Admin, tokenService);

        var expectedResponse = new GetUsersByIdResponse();

        var mockRepository = new Mock<IUserRepository>();
        var mockMapper = new Mock<IMapper>();

        mockRepository.Setup(r => r.Get(userId, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(user);

        mockMapper.Setup(m => m.Map<GetUsersByIdResponse>(user))
                      .Returns(expectedResponse);

        var handler = new GetUsersByIdHandler(mockRepository.Object, mockMapper.Object);
        var request = new GetUsersByIdRequest(userId);

        // Act
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(expectedResponse.Id, response.Id);
        Assert.Equal(expectedResponse.Username, response.Username);
    }

    [Fact]
    public async Task Handle_ShouldThrowInvalidOperationException_WhenUserNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();

        var mockRepository = new Mock<IUserRepository>();
        var mockMapper = new Mock<IMapper>();

        mockRepository.Setup(r => r.Get(userId, It.IsAny<CancellationToken>()))
                      .ReturnsAsync((Entities.User)null);

        var handler = new GetUsersByIdHandler(mockRepository.Object, mockMapper.Object);
        var request = new GetUsersByIdRequest(userId);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            handler.Handle(request, CancellationToken.None));

        Assert.Equal($"Usuario não encontrado. Id: {userId}", ex.Message);
    }
}