namespace TwitterUalaChallenge.Common.Errors;

public record ApiErrorType(int ErrorCode, string ErrorMessage)
{ 
    public static ApiErrorType UserAlreadyExists { get; } = new ApiErrorType(4, "Usuario existente");
    public static ApiErrorType UserNotFound { get; } = new ApiErrorType(5, "Usuario inexistente");
    public static ApiErrorType FollowingSelf { get; } = new ApiErrorType(6, "No puedes seguirte a ti mismo");
    public static ApiErrorType AlreadyFollowing { get; } = new ApiErrorType(7, "Ya estás siguiendo a este usuario");
    public static ApiErrorType NotFollowing { get; } = new ApiErrorType(8, "No estás siguiendo a este usuario");
}