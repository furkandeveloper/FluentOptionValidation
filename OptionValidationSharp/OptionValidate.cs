using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace OptionValidationSharp;

/// <summary>
/// Option Validate extension
/// </summary>
/// <typeparam name="TOptions">
/// The type of options being validated.
/// </typeparam>
public class OptionValidate<TOptions>(IServiceProvider serviceProvider, string? optionName) : IValidateOptions<TOptions>
    where TOptions : class
{
    /// <summary>
    /// Validate the options.
    /// </summary>
    /// <param name="name">
    /// The name of the options instance being validated, if any.
    /// </param>
    /// <param name="options">
    /// The options instance to validate.
    /// </param>
    /// <returns>
    /// The <see cref="ValidateOptionsResult" />.
    /// </returns>
    public ValidateOptionsResult Validate(string? name, TOptions options)
    {
        if (optionName is not null && optionName != name)
        {
            return ValidateOptionsResult.Skip;
        }
        
        ArgumentNullException.ThrowIfNull(options);

        using var scope = serviceProvider.CreateScope();
        
        var validator = scope.ServiceProvider.GetRequiredService<IValidator<TOptions>>();
        
        var result = validator.Validate(options);

        if (result.IsValid)
        {
            return ValidateOptionsResult.Success;
        }

        var type = options.GetType().Name;
        var errors = string.Join(", ", result.Errors.Select(x => x.ErrorMessage));
        var message = $"Options validation failed for type '{type}'. {errors}";
        return ValidateOptionsResult.Fail(message);
    }
}