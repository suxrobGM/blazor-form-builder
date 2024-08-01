using System.ComponentModel;

namespace FormBuilder.Models;

public enum ValidatorType
{
    [Description("Required Validator")]
    Required = 1,
    
    [Description("Length Validator")]
    Length = 2,
    
    [Description("Email Validator")]
    Email = 3,
    
    [Description("Numeric Range Validator")]
    NumericRange = 4,
}
