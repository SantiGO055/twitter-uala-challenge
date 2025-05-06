using MediatR;
using TwitterUalaChallenge.Application.Services.Interfaces;
using TwitterUalaChallenge.Common.DTOs;

namespace TwitterUalaChallenge.Application.UseCases.v1.Timeline.Queries;

public class GetTimelineByUserHandler(ITweetService tweetService) : IRequestHandler<GetTimelineByUserQuery, PaginatedResponse<TimelineResponse>>
{
    public async Task<PaginatedResponse<TimelineResponse>> Handle(GetTimelineByUserQuery request, CancellationToken cancellationToken)
    {
        return await tweetService.GetTimelineByUserAsync(request.UserId,request.Page, request.Limit);
    }
}