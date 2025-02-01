using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace OptionValidationSharp;

/// <summary>
/// Options builder extensions.
/// </summary>
public static class OptionsBuilderExtensions
{
    /// <summary>
    /// Validate the options.
    /// </summary>
    /// <param name="builder">
    /// The options' builder.
    /// </param>
    /// <typeparam name="TOptions">
    /// The type of options being validated.
    /// </typeparam>
    /// <returns>
    /// The <see cref="OptionsBuilder{TOptions}" />.
    /// </returns>
    public static OptionsBuilder<TOptions> Validate<TOptions>(this OptionsBuilder<TOptions> builder)
        where TOptions : class
    {
        builder.Services.AddSingleton<IValidateOptions<TOptions>>(
            serviceProvider => new OptionValidate<TOptions>(serviceProvider, builder.Name));
        return builder;
    }
}