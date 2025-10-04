using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.UpdateTask;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.CreateTask;
using System.Security.Claims;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.TaskComment.CreateTaskComment;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.UpdateTaskComment;
using ArquiteturaDesafio.Core.Application.UseCases.Queries.TaskComment.FilterById;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.DeleteTaskComment;

namespace ArquiteturaDesafio.General.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TasksCommentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksCommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(201)] // Criação
        [ProducesResponseType(400)] // Erro de validação
        [ProducesResponseType(401)] // Autenticação
        [ProducesResponseType(500)] // Erro interno
        [HttpPost("/TasksComment/{id}/Create")]
        public async Task<ActionResult<CreateTaskResponse>> Create([FromRoute] Guid id,
                                                             CreateTaskCommentRequest request,
                                                             CancellationToken cancellationToken)
        {
            request.SetTaskId(id);
            request.UpdateUserId(Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value));

            var response = await _mediator.Send(request, cancellationToken);
            return CreatedAtAction("Create", new { id = response.id }, response);
        }

        [HttpPut("/TasksComment/Update/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<UpdateTaskResponse>> Update([FromRoute]Guid id, [FromBody] UpdateTaskCommentRequest request,
                                                CancellationToken cancellationToken)
        {
            request.UpdateId(id);
            request.UpdateUserId(Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
            await _mediator.Send(request, cancellationToken);
            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<DeleteTaskCommentResponse>> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteTaskCommentRequest(id);
            await _mediator.Send(request, cancellationToken);
            return NoContent();
        }

        [HttpGet("/TasksComment/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<FilterTaskCommentByIdResponse>> GetById(Guid id,CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new FilterTaskCommentByIdRequest(id), cancellationToken);
            return Ok(response);
        }
    }
}
