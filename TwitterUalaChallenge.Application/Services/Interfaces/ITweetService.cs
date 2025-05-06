using TwitterUalaChallenge.Common.DTOs;
using TwitterUalaChallenge.Domain.Entities;

namespace TwitterUalaChallenge.Application.Services.Interfaces;

public interface ITweetService
{
    Task<Tweet> CreateContentAsync(Guid userId, string content);
    Task<PaginatedResponse<TimelineResponse>> GetTimelineByUserAsync(Guid userId, int page, int limit);
}