using MediatR;

namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.TaskComment.CreateTaskComment
{
    public class CreateTaskCommentRequest : IRequest<CreateTaskCommentResponse>
    {
        public Guid TaskId { get; internal set; }
        public Guid UserId { get; internal set; }
        public string Comment { get; set; }
        public void SetTaskId(Guid id)
        {
            TaskId = id;
        }

        public void UpdateUserId(Guid id)
        {
            UserId = id;
        }

        public void UpdateComment(string comment)
        {
            Comment = comment;
        }
    }
}
