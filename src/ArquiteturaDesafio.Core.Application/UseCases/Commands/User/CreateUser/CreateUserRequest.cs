using MediatR;
using ArquiteturaDesafio.Core.Application.UseCases.DTOs;

namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.User.CreateUser
{
    public class CreateUserRequest : UserDTO, IRequest<CreateUserResponse>
    {
        public Guid Id { get; internal set; }
        public void UpdateId(Guid id)
        {
            Id = id;
        }
    }
}
