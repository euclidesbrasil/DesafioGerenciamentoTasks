﻿using ArquiteturaDesafio.Core.Domain.Common;
using ArquiteturaDesafio.Core.Domain.Entities;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using ArquiteturaDesafio.Infrastructure.Persistence.PostgreSQL.Context;
using ArquiteturaDesafio.Infrastructure.Persistence.PostgreSQL.Extensions;
using ArquiteturaDesafio.Infrastructure.Persistence.PostgreSQL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ArquiteturaDesafio.Infrastructure.Persistence.PostgreSQL.Repositories
{
    public class TaskCommentRepository : BaseRepository<TaskComment>, ITaskCommentRepository
    {
        private readonly AppDbContext _context;
        public TaskCommentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
