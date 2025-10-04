﻿using ArquiteturaDesafio.Core.Domain.Common;
using ArquiteturaDesafio.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArquiteturaDesafio.Core.Domain.Interfaces
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        Task<PaginatedResult<Project>> GetProjectsPagination(PaginationQuery paginationQuery, CancellationToken cancellationToken);
    }
}
