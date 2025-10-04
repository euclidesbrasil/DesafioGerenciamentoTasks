using MediatR;

namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.AuthenticateUser
{
    public class AuthenticateUserRequest : IRequest<AuthenticateUserResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
