namespace AuthService.Application.Exceptions;

public static class ErrorCodes
{
    public const string EMAIL_ALREADY_EXISTS = "EMAIL_ALREADY_EXISTS";
    public const string USERNAME_ALREADY_EXISTS = "USERNAME_ALREADY_EXISTS";
    public const string INVALID_CREDENTIALS = "INVALID_CREDENTIALS";
    public const string USER_ACCOUNT_DISABLED = "USER_ACCOUNT_DISABLED";
    public const string IMAGE_UPLOAD_FAILED = "IMAGE_UPLOAD_FAILED";
    public const string INVALID_FILE_FORMAT = "INVALID_FILE_FORMAT";
    public const string FILE_TOO_LARGE = "FILE_TOO_LARGE";
}


// EMAIL_ALREADY_EXISTS        → El correo electrónico ya existe
// USERNAME_ALREADY_EXISTS     → El nombre de usuario ya existe
// INVALID_CREDENTIALS         → Credenciales inválidas
// USER_ACCOUNT_DISABLED       → La cuenta de usuario está deshabilitada
// IMAGE_UPLOAD_FAILED         → Falló la subida de la imagen
// INVALID_FILE_FORMAT         → Formato de archivo inválido
// FILE_TOO_LARGE              → El archivo es demasiado grande