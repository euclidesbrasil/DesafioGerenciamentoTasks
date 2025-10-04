﻿using AutoMapper;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.User.UpdateUser;
using ArquiteturaDesafio.Core.Application.UseCases.DTOs;
using ArquiteturaDesafio.Core.Domain.Entities;
using ArquiteturaDesafio.Core.Domain.ValueObjects;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.User.CreateUser;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.User.DeleteUser;
using Entities = ArquiteturaDesafio.Core.Domain.Entities;
using ArquiteturaDesafio.Core.Application.UseCases.Queries.GetUsersQuery;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.User.CreateProject;
using ArquiteturaDesafio.Core.Application.UseCases.Commands.Task.CreateTask;
namespace ArquiteturaDesafio.Core.Application.UseCases.Mapper
{
    public class CommonMapper : Profile
    {
        public CommonMapper()
        {
            CreateMap<Address, AddressDto>();
            CreateMap<AddressDto, Address>();


            CreateMap<GeolocationDto, Geolocation>()
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Lat))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Long));

            CreateMap<Geolocation, GeolocationDto>()
            .ForMember(dest => dest.Lat, opt => opt.MapFrom(src => src.Latitude))
            .ForMember(dest => dest.Long, opt => opt.MapFrom(src => src.Longitude));

            CreateMap<CreateUserRequest, User>();
            CreateMap<User, CreateUserResponse>();
            CreateMap<DeleteUserRequest, User>();
            CreateMap<User, DeleteUserRequest>();
            CreateMap<User, GetUsersQueryResponse>();
            CreateMap<User, UserDTO>();
            CreateMap<User, UpdateUserResponse>();
            CreateMap<UserDTO, UpdateUserResponse>();
            CreateMap<CreateProjectRequest, Entities.Project>();
            CreateMap<CreateTaskRequest, Entities.Task>();
            
            CreateMap<Entities.Task, Entities.Task>();
        }
    }
}