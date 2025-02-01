using FluentValidation;

namespace Sample.Api;

public class AppSettingsValidator : AbstractValidator<AppSettings>
{
    public AppSettingsValidator()
    {
        RuleFor(r => r.ApplicationName).NotEmpty();
        RuleFor(r => r.MaxUsers).InclusiveBetween(1,100);
        RuleFor(r => r.Url).NotEmpty();
        RuleFor(r => r.Url)
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
            .When(r => !string.IsNullOrWhiteSpace(r.Url));
    }
}