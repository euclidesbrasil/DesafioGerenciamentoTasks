using AutoMapper;

using ArquiteturaDesafio.Core.Domain.Entities;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using ArquiteturaDesafio.Core.Domain.ValueObjects;
using MediatR;
using ArquiteturaDesafio.Core.Domain.Enum;
using System.Threading;

namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.User.CreateProject;

public class CreateProjectHandler :
       IRequestHandler<CreateProjectRequest, CreateProjectResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProjectRepository _repository;
    private readonly IMapper _mapper;

    public CreateProjectHandler(IUnitOfWork unitOfWork,
        IProjectRepository userRepository,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _repository = userRepository;
        _mapper = mapper;
    }

    public async Task<CreateProjectResponse> Handle(CreateProjectRequest request,
        CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ArquiteturaDesafio.Core.Domain.Entities.Project>(request);
        _repository.Create(entity);
        await _unitOfWork.Commit(cancellationToken);
        return new CreateProjectResponse(entity.Id);
    }
}
