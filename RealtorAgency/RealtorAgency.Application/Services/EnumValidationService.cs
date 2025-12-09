using RealtorAgency.Domain.Enums;

namespace RealtorAgency.Application.Services;

/// <inheritdoc/>
public class EnumValidationService : IEnumValidationService
{
    public bool ValidateRequestType(string type, out string errorMessage)
    {
        return ValidateEnum(typeof(RequestType), type, out errorMessage);
    }

    public bool ValidatePropertyType(string type, out string errorMessage)
    {
        return ValidateEnum(typeof(PropertyType), type, out errorMessage);
    }

    public bool ValidatePurpose(string purpose, out string errorMessage)
    {
        return ValidateEnum(typeof(Purpose), purpose, out errorMessage);
    }

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
