﻿namespace ArquiteturaDesafio.Core.Application.UseCases.DTOs
{
    public class ProjectBaseDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get;  set; }
        public string Description { get;  set; }
    }
} 