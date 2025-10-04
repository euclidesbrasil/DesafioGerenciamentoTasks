using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ArquiteturaDesafio.Core.Application.UseCases.Queries.GetAverageCompletedTasksQuery;

namespace ArquiteturaDesafio.General.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<GetAverageCompletedTasksQueryResponse>> GetAverageCompletedTasksQuery(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAverageCompletedTasksQueryRequest(), cancellationToken);
            return Ok(response);
        }
    }
}
