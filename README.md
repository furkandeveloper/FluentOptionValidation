## Readme
<p align="center">
  <img src="https://github.com/user-attachments/assets/215e58b1-12f1-4c1a-b22f-85fe9de2b6be" style="max-width:100%;" height="140" />
</p>


## Give a Star 🌟
If you liked the project or if **FluentOptionValidation** helped you, please give a star.

### Purpose
**FluentValidationOption(OptionValidationSharp)** provides option validation with Fluent Validation.

### How To Use(?)

### Install

```bash
dotnet add package OptionValidationSharp
```
Example appsettings.json file

```json
"AppSettings": {
    "ApplicationName": "MyApp",
    "MaxUsers": 500,
    "Url": "https://github.com"
  }
```

Example Option Class

```csharp
public class AppSettings
{
    public string? ApplicationName { get; set; }
    
    public int MaxUsers { get; set; }
    
    public string? Url { get; set; }
}
```

Example Option Validation Class

```csharp
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
```

Use in Program.cs

```csharp
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddOptions<AppSettings>()
    .Bind(builder.Configuration.GetSection("AppSettings"))
    .ValidateOptionSharp() // <- 🔨 Validation Option with Fluent Validation
    .ValidateOnStart();
```

Result:

```bash
fail: Microsoft.Extensions.Hosting.Internal.Host[11]
Hosting failed to start
Microsoft.Extensions.Options.OptionsValidationException: Options validation failed for type 'AppSettings'. 'Max Users' must be between 1 and 100. You entered 500.
```
