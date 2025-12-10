using RealtorAgency.Domain.Enums;

namespace RealtorAgency.Api;

/// <summary>
/// Provides static enum validation operations.
/// </summary>
public static class EnumValidator
{
    /// <summary>
    /// Validates if the specified request type exists.
    /// </summary>
    /// <param name="type">The request type to validate.</param>
    /// <param name="errorMessage">Error message if validation fails.</param>
    /// <returns>True if valid, false otherwise.</returns>
    public static bool ValidateRequestType(string type, out string errorMessage)
    {
        return ValidateEnum(typeof(RequestType), type, out errorMessage);
    }

    /// <summary>
    /// Validates if the specified property type exists.
    /// </summary>
    /// <param name="type">The property type to validate.</param>
    /// <param name="errorMessage">Error message if validation fails.</param>
    /// <returns>True if valid, false otherwise.</returns>
    public static bool ValidatePropertyType(string type, out string errorMessage)
    {
        return ValidateEnum(typeof(PropertyType), type, out errorMessage);
    }

    /// <summary>
    /// Validates if the specified purpose exists.
    /// </summary>
    /// <param name="purpose">The purpose to validate.</param>
    /// <param name="errorMessage">Error message if validation fails.</param>
    /// <returns>True if valid, false otherwise.</returns>
    public static bool ValidatePurpose(string purpose, out string errorMessage)
    {
        return ValidateEnum(typeof(Purpose), purpose, out errorMessage);
    }

    /// <summary>
    /// Generic enum validation.
    /// </summary>
    private static bool ValidateEnum(Type enumType, string value, out string errorMessage)
    {
        errorMessage = string.Empty;

        if (Enum.IsDefined(enumType, value))
            return true;

        var validValues = string.Join(", ", Enum.GetNames(enumType));
        errorMessage = $"Invalid {enumType.Name}: '{value}'. Allowed values: {validValues}";

        return false;
    }
}
