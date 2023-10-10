using Codend.Application.Core.Abstractions.Authentication;
using Codend.Application.Core.Abstractions.Data;
using Codend.Application.Core.Abstractions.Messaging.Commands;
using Codend.Application.ProjectTasks.Commands.CreateProjectTask.Abstractions;
using Codend.Domain.Entities.ProjectTask.Bugfix;
using Codend.Domain.Repositories;

namespace Codend.Application.ProjectTasks.Commands.CreateProjectTask;

/// <summary>
/// Command for creating <see cref="BugfixProjectTask"/>.
/// </summary>
/// <param name="TaskProperties">BugfixProjectTask properties.</param>
public sealed record CreateBugfixProjectTaskCommand
(
    BugfixProjectTaskCreateProperties TaskProperties
) : ICommand<Guid>, ICreateProjectTaskCommand<BugfixProjectTaskCreateProperties>;

/// <summary>
/// <see cref="CreateBugfixProjectTaskCommand"/> handler.
/// </summary>
public class CreateBugfixProjectTaskCommandHandler :
    CreateProjectTaskCommandAbstractHandler<
        CreateBugfixProjectTaskCommand,
        BugfixProjectTask,
        BugfixProjectTaskCreateProperties>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateBugfixProjectTaskCommandHandler"/> class.
    /// </summary>
    public CreateBugfixProjectTaskCommandHandler(
        IProjectTaskRepository projectTaskRepository,
        IUnitOfWork unitOfWork,
        IHttpContextProvider identityProvider,
        IProjectMemberRepository projectMemberRepository,
        IStoryRepository storyRepository,
        IProjectTaskStatusRepository statusRepository)
        : base(projectTaskRepository,
            unitOfWork,
            identityProvider,
            projectMemberRepository,
            storyRepository,
            statusRepository)
    {
    }
}