using MediatR;

namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.User.DeleteUser;

public record DeleteUserRequest(Guid Id) : IRequest<DeleteUserResponse>
{
}
