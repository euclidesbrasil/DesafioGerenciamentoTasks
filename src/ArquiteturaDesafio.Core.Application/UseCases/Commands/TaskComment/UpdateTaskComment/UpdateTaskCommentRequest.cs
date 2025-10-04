using MediatR;
namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.UpdateTaskComment
{
    public class UpdateTaskCommentRequest : IRequest<UpdateTaskCommentResponse>
    {
        public Guid Id { get; internal set; }
        public Guid UserId { get; internal set; }
        public string Comment { get; set; }
        public void UpdateId(Guid id)
        {
            Id = id;
        }

        public void UpdateUserId(Guid id)
        {
            UserId = id;
        }

        public void UpdateInfos(string comment, Guid idUser)
        {
            Comment = comment;
            UserId = idUser;
        }
    }
}
