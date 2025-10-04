using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.Project.UpdateProject;
using ArquiteturaDesafio.Core.Application.UseCases.Queries.Project.FilterById;
using ArquiteturaDesafio.Core.Application.UseCases.Queries.Project.FilterQuery;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.User.CreateProject;
using ArquiteturaDesafio.Application.UseCases.Commands.User.DeleteProject;

namespace ArquiteturaDesafio.General.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(201)] // Criação
        [ProducesResponseType(400)] // Erro de validação
        [ProducesResponseType(401)] // Autenticação
        [ProducesResponseType(500)] // Erro interno
        public async Task<ActionResult<CreateProjectResponse>> Create([FromBody] CreateProjectRequest request,
                                                             CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return CreatedAtAction("Create", new { id = response.id }, response);
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<UpdateProjectResponse>> Update(Guid id, [FromBody] UpdateProjectRequest request,
                                                CancellationToken cancellationToken)
        {
            request.SetId(id);
            await _mediator.Send(request, cancellationToken);
            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<UpdateProjectResponse>> Delete([FromRoute]Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteProjectRequest(id);
            await _mediator.Send(request, cancellationToken);
            return NoContent();
        }


        [HttpGet("/Projects/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<FilterProjectByIdResponse>> GetById(Guid id,CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new FilterProjectByIdRequest(id), cancellationToken);
            return Ok(response);
        }

        [HttpGet("/Projects")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<GetProjectsQueryResponse>>> GetProjectsQuery(CancellationToken cancellationToken, int _page = 1, int _size = 10, [FromQuery] Dictionary<string, string> filters = null, string _order = "id asc")
        {
            filters = filters ?? new Dictionary<string, string>();
             filters = HttpContext.Request.Query
            .Where(q => q.Key != "_page" && q.Key != "_size" && q.Key != "_order")
            .ToDictionary(q => q.Key, q => q.Value.ToString());

            var response = await _mediator.Send(new GetProjectsQueryRequest(_page, _size, _order, filters), cancellationToken);
            return Ok(response);
        }
    }
}
