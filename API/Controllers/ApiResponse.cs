namespace API.Controllers;

public record ApiResponse(object? Data, object? Error = null)
{
    public bool Success => Error is null;
};

public record ApiResponse<T>(T? Data, string? Error = null) {
    public bool Success => Error is null;
};

