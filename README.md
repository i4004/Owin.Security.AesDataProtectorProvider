# Owin.Security.AesDataProtectorProvider

[![Nuget Version](https://img.shields.io/nuget/v/Owin.Security.AesDataProtectorProvider)](https://www.nuget.org/packages/Owin.Security.AesDataProtectorProvider)
[![Nuget Download](https://img.shields.io/nuget/dt/Owin.Security.AesDataProtectorProvider)](https://www.nuget.org/packages/Owin.Security.AesDataProtectorProvider)
[![Build Package](https://github.com/i4004/Owin.Security.AesDataProtectorProvider/actions/workflows/build.yml/badge.svg)](https://github.com/i4004/Owin.Security.AesDataProtectorProvider/actions/workflows/build.yml)[![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Owin.Security.AesDataProtectorProvider)](https://libraries.io/nuget/Owin.Security.AesDataProtectorProvider)
[![CodeFactor Grade](https://img.shields.io/codefactor/grade/github/i4004/Owin.Security.AesDataProtectorProvider)](https://www.codefactor.io/repository/github/i4004/Owin.Security.AesDataProtectorProvider)
![Platform](https://img.shields.io/badge/platform-.NET%204.5-lightgrey)

`Owin.Security.AesDataProtectorProvider` - is an AES cryptic provider for OWIN authentication middlewares.
It is based on managed and CSP .Net framework providers.

## Examples

### Registration

```csharp
public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        ...
        app.UseAesDataProtectorProvider();
        ...
    }
}
```

#### Usage with custom key

```csharp
...
app.UseAesDataProtectorProvider("my key");
...
```

#### Enabling usage with FIPS-compliant CSP provider

```csharp
...
app.UseAesDataProtectorProvider(null, true);
...
```

or

```csharp
...
app.UseAesDataProtectorProvider("my key", true);
...
```

### Usage example with cookie authentication

```csharp
public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        app.UseCookieAuthentication(new CookieAuthenticationOptions
        {
            AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            LoginPath = new PathString("/login")
        });

        app.UseAesDataProtectorProvider();
    }
}
```
