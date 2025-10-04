using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.UpdateTask;
using ArquiteturaDesafio.Core.Application.UseCases.Queries.Task.FilterById;
using ArquiteturaDesafio.Core.Application.UseCases.Queries.Task.FilterQuery;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.CreateTask;
using System.Security.Claims;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.DeleteTask;

namespace ArquiteturaDesafio.General.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(201)] // Criação
        [ProducesResponseType(400)] // Erro de validação
        [ProducesResponseType(401)] // Autenticação
        [ProducesResponseType(500)] // Erro interno
        public async Task<ActionResult<CreateTaskResponse>> Create(CreateTaskRequest request,
                                                             CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return CreatedAtAction("Create", new { id = response.id }, response);
        }

        [HttpPut("Update/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<UpdateTaskResponse>> Update([FromRoute]Guid id, [FromBody] UpdateTaskRequest request,
                                                CancellationToken cancellationToken)
        {
            request.UpdateId(id);
            request.SetUser(Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value));

            await _mediator.Send(request, cancellationToken);
            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<DeleteTaskResponse>> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteTaskRequest(id);
            await _mediator.Send(request, cancellationToken);
            return NoContent();
        }

        [HttpGet("/Tasks/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<FilterTaskByIdResponse>> GetById(Guid id,CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new FilterTaskByIdRequest(id), cancellationToken);
            return Ok(response);
        }

        [HttpGet("/Tasks")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<GetTasksQueryResponse>>> GetTasksQuery(CancellationToken cancellationToken, int _page = 1, int _size = 10, [FromQuery] Dictionary<string, string> filters = null, string _order = "id asc")
        {
            filters = filters ?? new Dictionary<string, string>();
             filters = HttpContext.Request.Query
            .Where(q => q.Key != "_page" && q.Key != "_size" && q.Key != "_order")
            .ToDictionary(q => q.Key, q => q.Value.ToString());

            var response = await _mediator.Send(new GetTasksQueryRequest(_page, _size, _order, filters), cancellationToken);
            return Ok(response);
        }
    }
}
