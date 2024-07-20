namespace FormBuilder.API.Models;

/// <summary>
/// Represents the result of an operation
/// </summary>
public interface IResult
{   
    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    bool Success { get; }
    
    /// <summary>
    /// Gets the error message.
    /// </summary>
    string? Error { get; init; }
}
