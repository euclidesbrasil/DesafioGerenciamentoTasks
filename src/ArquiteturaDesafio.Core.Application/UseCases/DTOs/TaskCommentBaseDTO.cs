namespace ArquiteturaDesafio.Core.Application.UseCases.DTOs
{
    public class TaskCommentBaseDTO
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}