Owin.Security.AesDataProtectorProvider
===

`Owin.Security.AesDataProtectorProvider` - is an AES cryptic provider for OWIN authentication middlewares.
It is based on managed and CSP .Net framework providers.

## Package status

| Latest version | [![Nuget version](http://img.shields.io/badge/nuget-v1.1-blue.png)](https://www.nuget.org/packages/Owin.Security.AesDataProtectorProvider/) |
| :------ | :------: |
| **Dependencies** | [![NuGet Status](http://nugetstatus.com/Owin.Security.AesDataProtectorProvider.png)](http://nugetstatus.com/packages/Owin.Security.AesDataProtectorProvider) |

## Issues status

| Ready issues |
| :------ |
| [![Stories in Ready](https://badge.waffle.io/i4004/Owin.Security.AesDataProtectorProvider.png?label=ready&title=Ready)](https://waffle.io/i4004/Owin.Security.AesDataProtectorProvider) |

## Build status

| Platform | Status of last build |
| :------ | :------: |
| **.NET (4.5)** | [![AppVeyor build status](https://ci.appveyor.com/api/projects/status/0vjtl572q4f8nh3r/branch/master?svg=true)](https://ci.appveyor.com/project/i4004/owin-security-aesdataprotectorprovider) |
| **Mono (Latest)** | [![Travis build status](https://travis-ci.org/i4004/Owin.Security.AesDataProtectorProvider.png?branch=master)](https://travis-ci.org/i4004/Owin.Security.AesDataProtectorProvider) |

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

##### Usage with custom key:
```csharp
...
app.UseAesDataProtectorProvider("my key");
...
```

##### Enabling usage with FIPS-compliant CSP provider:
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
