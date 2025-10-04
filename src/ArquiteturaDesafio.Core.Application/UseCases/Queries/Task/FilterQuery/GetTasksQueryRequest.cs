using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArquiteturaDesafio.Core.Application.UseCases.Queries.Task.FilterQuery
{
    public sealed record GetTasksQueryRequest(int page, int size, string order, Dictionary<string, string> filters = null) : IRequest<GetTasksQueryResponse>;
}
