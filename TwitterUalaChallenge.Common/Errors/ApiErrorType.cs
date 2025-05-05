namespace TwitterUalaChallenge.Common.Errors;

public record ApiErrorType(int ErrorCode, string ErrorMessage)
{
    public static ApiErrorType NotDefined { get; } = new ApiErrorType(0, "Error no definido");
    public static ApiErrorType NotCreated { get; } = new ApiErrorType(1, "Error al crear la entidad");
    public static ApiErrorType WrongEntityData { get; } = new ApiErrorType(2, "Datos inválidos");
    public static ApiErrorType EntityNotExist { get; } = new ApiErrorType(3, "Entidad inexistente");
    public static ApiErrorType UserAlreadyExists { get; } = new ApiErrorType(4, "Usuario existente");
    public static ApiErrorType UserNotFound { get; } = new ApiErrorType(5, "Usuario inexistente");
}