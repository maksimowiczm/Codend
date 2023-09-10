using Codend.Domain.Entities;
using Codend.Domain.Repositories;

namespace Codend.Persistence.Repositories;

public class ProjectTaskRepository : GenericRepository<ProjectTaskId, Guid, AbstractProjectTask>, IProjectTaskRepository
{
    public ProjectTaskRepository(CodendApplicationDbContext context) : base(context)
    {
    }

    public bool ProjectTaskIsValid(ProjectId projectId, ProjectTaskStatusId statusId)
    {
        var valid =
            Context.Set<ProjectTaskStatus>()
            .Any(status => status.ProjectId == projectId && status.Id == statusId);

        return valid;
    }
}