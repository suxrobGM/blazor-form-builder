using System.Text.Json.Serialization;

namespace FormBuilder.API.Models;

/// <summary>
/// Represents the result of an operation.
/// </summary>
public class Result : IResult
{
    /// <summary>
    /// Gets the error message.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Error { get; init; }

    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool Success => string.IsNullOrEmpty(Error);

    /// <summary>
    /// Creates a new instance of <see cref="Result"/> with <see cref="Success"/> set to <c>true</c>.
    /// </summary>
    public static Result Succeed() => new();
    
    /// <summary>
    /// Creates a new instance of <see cref="Result"/> with <see cref="Success"/> set to <c>false</c>.
    /// </summary>
    /// <param name="error">The error message to be returned in the Result object</param>
    public static Result Fail(string error) => new() { Error = error };
}

/// <summary>
/// Represents the result of an operation with a data payload.
/// </summary>
/// <typeparam name="T">The datatype of the payload</typeparam>
public class Result<T> : Result
{
    /// <summary>
    /// Gets the data payload.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public T? Data { get; init; }

    /// <summary>
    /// Creates a new instance of <see cref="Result{T}"/> with <see cref="Succeed"/> set to <c>true</c>.
    /// </summary>
    /// <param name="result">The data to be returned in the Result object</param>
    public static Result<T> Succeed(T result) => new() { Data = result };
    
    /// <summary>
    /// Creates a new instance of <see cref="Result"/> with <see cref="Succeed"/> set to <c>false</c>.
    /// </summary>
    /// <param name="error">The error message to be returned in the Result object</param>
    public new static Result<T> Fail(string error) => new() { Error = error };
}
