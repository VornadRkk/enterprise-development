namespace RealtorAgency.Application;

/// <summary>
/// Provides enum validation operations.
/// </summary>
public interface IEnumValidationService
{
    /// <summary>   
    /// Validates if the specified request type exists.
    /// </summary>
    /// <param name="type">The request type to validate.</param>
    /// <param name="errorMessage">Error message if validation fails.</param>
    /// <returns>True if valid, false otherwise.</returns>
    public bool ValidateRequestType(string type, out string errorMessage);

    /// <summary>
    /// Validates if the specified property type exists.
    /// </summary>
    /// <param name="type">The property type to validate.</param>
    /// <param name="errorMessage">Error message if validation fails.</param>
    /// <returns>True if valid, false otherwise.</returns>
    public bool ValidatePropertyType(string type, out string errorMessage);

    /// <summary>
    /// Validates if the specified purpose exists.
    /// </summary>
    /// <param name="purpose">The purpose to validate.</param>
    /// <param name="errorMessage">Error message if validation fails.</param>
    /// <returns>True if valid, false otherwise.</returns>
    public bool ValidatePurpose(string purpose, out string errorMessage);
}
