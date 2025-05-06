using TwitterUalaChallenge.Common.DTOs;
using TwitterUalaChallenge.Contracts.Core.Application;

namespace TwitterUalaChallenge.Application.UseCases.v1.Timeline.Queries;

public class GetTimelineByUserQuery(Guid userId, int page, int limit): Request<PaginatedResponse<TimelineResponse>>
{
    public Guid UserId { get; set; } = userId;
    public int Page { get; set; } = page;
    public int Limit { get; set; } = limit;

    public override bool ExecuteSaveChanges() => false;
}