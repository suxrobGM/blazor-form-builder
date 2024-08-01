using FormBuilder.Models;

namespace FormBuilder.Factories;

/// <summary>
/// Factory class for creating validators.
/// </summary>
public static class ValidatorFactory
{
    /// <summary>
    /// Creates a validator based on the given type.
    /// </summary>
    /// <param name="validatorType">
    /// The type of the validator to create.
    /// </param>
    /// <returns>
    /// A new instance of the validator based on the provided type.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the provided validator type is not recognized.
    /// </exception>
    public static Validator Create(ValidatorType validatorType)
    {
        return validatorType switch
        {
            ValidatorType.Required => new RequiredValidator(),
            ValidatorType.Length => new LengthValidator(),
            ValidatorType.Email => new EmailValidator(),
            ValidatorType.NumericRange => new NumericRangeValidator(),
            _ => throw new ArgumentOutOfRangeException(nameof(validatorType), validatorType, null)
        };
    }
}
