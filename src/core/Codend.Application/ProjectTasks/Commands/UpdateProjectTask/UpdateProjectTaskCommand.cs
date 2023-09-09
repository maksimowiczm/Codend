using Codend.Application.Core.Abstractions.Data;
using Codend.Application.Core.Abstractions.Messaging.Commands;
using Codend.Contracts.ProjectTasks;
using Codend.Domain.Core.Errors;
using Codend.Domain.Entities;
using Codend.Domain.Repositories;
using Codend.Shared.ShouldUpdate;
using FluentResults;

namespace Codend.Application.ProjectTasks.Commands.UpdateProjectTask;

public interface IUpdateProjectTaskCommand<out TUpdateProjectTaskProperties>
    where TUpdateProjectTaskProperties : UpdateProjectTaskProperties
{
    ProjectTaskId TaskId { get; }
    TUpdateProjectTaskProperties UpdateTaskProperties { get; }
}

public sealed record UpdateProjectTaskCommand
(
    ProjectTaskId TaskId,
    UpdateProjectTaskProperties UpdateTaskProperties
) : ICommand, IUpdateProjectTaskCommand<UpdateProjectTaskProperties>;

public class UpdateProjectTaskCommandHandler<TCommand, TProjectTask, TUpdateProjectTaskProperties>
    : ICommandHandler<TCommand>
    where TCommand : ICommand, IUpdateProjectTaskCommand<TUpdateProjectTaskProperties>
    where TProjectTask : ProjectTask, IProjectTaskUpdater<TProjectTask, TUpdateProjectTaskProperties>
    where TUpdateProjectTaskProperties : UpdateProjectTaskProperties
{
    private readonly IProjectTaskRepository _taskRepository;
    private readonly IUnitOfWork _unitOfWork;

    protected UpdateProjectTaskCommandHandler(IProjectTaskRepository taskRepository, IUnitOfWork unitOfWork)
    {
        _taskRepository = taskRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(TCommand request, CancellationToken cancellationToken)
    {
        if (await _taskRepository.GetByIdAsync(request.TaskId) is not TProjectTask task)
        {
            return Result.Fail(new DomainErrors.ProjectTaskErrors.ProjectTaskNotFound());
        }

        task.Update(request.UpdateTaskProperties);

        _taskRepository.Update(task);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}

public static class UpdateProjectTaskExtensions
{
    /// <summary>
    /// 💀👽
    /// </summary>
    public static UpdateProjectTaskCommand MapToCommand(this UpdateProjectTaskRequest request)
    {
        var name = request.Name ?? new ShouldUpdateProperty<string>(false);
        var priority = request.Priority ?? new ShouldUpdateProperty<string>(false);
        var statusId = request.StatusId is null
            ? new ShouldUpdateProperty<ProjectTaskStatusId> { ShouldUpdate = false }
            : new ShouldUpdateProperty<ProjectTaskStatusId>
                { ShouldUpdate = true, Value = new ProjectTaskStatusId(request.StatusId.Value) };
        var description = request.Description ?? new ShouldUpdateProperty<string?>(false);
        var estimatedTime = request.EstimatedTime is null
            ? new ShouldUpdateProperty<TimeSpan?>(false)
            : new ShouldUpdateProperty<TimeSpan?>
            {
                ShouldUpdate = true,
                Value = request.EstimatedTime.Value is null
                    ? null
                    : new TimeSpan(
                        (int)request.EstimatedTime.Value.Days,
                        (int)request.EstimatedTime.Value.Hours,
                        (int)request.EstimatedTime.Value.Minutes,
                        0)
            };
        var dueDate = request.DueDate ?? new ShouldUpdateProperty<DateTime?>(false);
        var storyPoints = request.StoryPoints ?? new ShouldUpdateProperty<uint?>(false);
        var assigneeId = request.AssigneeId is null
            ? new ShouldUpdateProperty<UserId?> { ShouldUpdate = false }
            : new ShouldUpdateProperty<UserId?>
            {
                ShouldUpdate = true,
                Value = request.AssigneeId.Value is null
                    ? null
                    : new UserId(request.AssigneeId.Value.Value) // XD
            };
        // D:

        var command = new UpdateProjectTaskCommand(
            new ProjectTaskId(request.TaskId),
            new BugfixUpdateProjectTaskProperties(
                name,
                priority,
                statusId,
                description,
                estimatedTime,
                dueDate,
                storyPoints,
                assigneeId
            )
        );

        return command;
    }
}