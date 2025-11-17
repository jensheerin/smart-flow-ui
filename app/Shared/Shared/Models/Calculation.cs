namespace Shared.Models;

/// <summary>
/// Represents an engineering calculation performed based on extracted parameters.
/// </summary>
public class Calculation
{
    /// <summary>
    /// Gets or sets the unique calculation identifier.
    /// </summary>
    public required string CalculationId { get; set; }

    /// <summary>
    /// Gets or sets the type of calculation performed.
    /// </summary>
    public required string CalculationType { get; set; }

    /// <summary>
    /// Gets or sets the input parameters used in the calculation.
    /// </summary>
    public required Dictionary<string, string> InputParameters { get; set; } = new();

    /// <summary>
    /// Gets or sets the formula or method used for the calculation.
    /// </summary>
    public required string FormulaUsed { get; set; }

    /// <summary>
    /// Gets or sets the calculated result value.
    /// </summary>
    public required string ResultValue { get; set; }

    /// <summary>
    /// Gets or sets the unit of measurement for the result.
    /// </summary>
    public string? ResultUnit { get; set; }

    /// <summary>
    /// Gets or sets the validation status of the calculation.
    /// </summary>
    public required ValidationStatus ValidationStatus { get; set; }

    /// <summary>
    /// Gets or sets the explanation or step-by-step breakdown.
    /// </summary>
    public required string Explanation { get; set; }

    /// <summary>
    /// Gets or sets any warnings or notes about the calculation.
    /// </summary>
    public string? CalculationWarnings { get; set; }
}

/// <summary>
/// Represents the validation status of a calculation.
/// </summary>
public enum ValidationStatus
{
    /// <summary>Calculation passed validation checks.</summary>
    Valid,

    /// <summary>Calculation has warnings but is acceptable.</summary>
    Warning,

    /// <summary>Calculation failed validation checks.</summary>
    Invalid,

    /// <summary>Calculation could not be validated.</summary>
    Unknown
}
