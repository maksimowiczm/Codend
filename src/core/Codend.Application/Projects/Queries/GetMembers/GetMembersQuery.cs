using Codend.Application.Core.Abstractions.Authentication;
using Codend.Application.Core.Abstractions.Data;
using Codend.Application.Core.Abstractions.Messaging.Queries;
using Codend.Application.Core.Abstractions.Querying;
using Codend.Application.Core.Abstractions.Services;
using Codend.Application.Extensions;
using Codend.Contracts.Responses;
using Codend.Domain.Entities;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using static Codend.Domain.Core.Errors.DomainErrors.General;

namespace Codend.Application.Projects.Queries.GetMembers;

/// <summary>
/// Query for retrieving all project members of project.
/// </summary>
/// <param name="ProjectId">Id of the project whose members will be retrieved.</param>
/// <param name="Search">Text to be searched for in project title.</param>
public sealed record GetMembersQuery
(
    ProjectId ProjectId,
    string? Search = null
) : IQuery<IEnumerable<UserResponse>>, ITextSearchQuery;

/// <summary>
/// <see cref="GetMembersQueryHandler"/> Handler.
/// </summary>
public class GetMembersQueryHandler : IQueryHandler<GetMembersQuery, IEnumerable<UserResponse>>
{
    private readonly IHttpContextProvider _identityProvider;
    private readonly IQueryableSets _queryableSets;
    private readonly IUserService _userService;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetMembersQueryHandler"/> class.
    /// </summary>
    public GetMembersQueryHandler(
        IHttpContextProvider identityProvider,
        IQueryableSets queryableSets,
        IUserService userService)
    {
        _identityProvider = identityProvider;
        _queryableSets = queryableSets;
        _userService = userService;
    }

    /// <inheritdoc />
    public async Task<Result<IEnumerable<UserResponse>>> Handle(GetMembersQuery query,
        CancellationToken cancellationToken)
    {
        var userId = _identityProvider.UserId;
        if (await _queryableSets.Queryable<ProjectMember>().IsUserMember(userId, query.ProjectId) is false)
        {
            return DomainNotFound.Fail<Project>();
        }

        var usersIds = await _queryableSets.Queryable<ProjectMember>()
            .Where(x => x.ProjectId == query.ProjectId)
            .Select(x => x.MemberId)
            .ToListAsync(cancellationToken);

        var usersResponse = await _userService.GetUsersByIds(usersIds);

        if (query.Search != null)
        {
            usersResponse = usersResponse.Where(user => user.ToString().Contains(query.Search)).ToList();
        }

        return Result.Ok(usersResponse);
    }
}