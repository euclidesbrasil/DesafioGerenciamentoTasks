using AutoMapper;
using ArquiteturaDesafio.Core.Domain.Interfaces;
using MediatR;

namespace ArquiteturaDesafio.Core.Application.UseCases.Commands.Project.UpdateProject;

public class UpdateProjectHandler :
       IRequestHandler<UpdateProjectRequest, UpdateProjectResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProjectRepository _repository;

    public UpdateProjectHandler(IUnitOfWork unitOfWork,
        IProjectRepository userRepository,
        IMapper mapper
        )
    {
        _unitOfWork = unitOfWork;
        _repository = userRepository;
    }

    public async Task<UpdateProjectResponse> Handle(UpdateProjectRequest request,
        CancellationToken cancellationToken)
    {
        var project = await _repository.Get(request.Id, cancellationToken);
        if (project is null)
        {
            throw new InvalidOperationException($"Project não encontrado. Id: {request.Id}");
        }

        project.UpdateInfo(request.Title, request.Description);
        project.SetUser(request.UserId);
        _repository.Update(project);
        await _unitOfWork.Commit(cancellationToken);
        return new UpdateProjectResponse();
    }
}
