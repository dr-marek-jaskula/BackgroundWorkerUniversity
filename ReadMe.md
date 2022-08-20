## Background Worker University

In this solution the Background Worker is presented for the learning purpose. Both the basics and advanced issues are discussed.

In basic Background Worker template we have dependency injection and standard logging out of the box.

## Serilog

In this project we use Serilog logger (we use Serilog.Extensions.Hosting for workers and console projects).
We add two sinks: 
Serilog.Sinks.Console
Serilog.Sinks.File

## Covarel

In this project we use the Coravel NuGet Package to obtain a well designed scheduling.

## FluentEmail.Core 

For the purpose of sending the emails we use "FluentEmail.Core" (domain models) and "FluentEmail.Smtp" (to send emails) because it is simple and fast way to send emails.

We use the "FluentEmail.Razor" for mail templates

Add to the project file:
```
<PreserveCompilationContext>true</PreserveCompilationContext>
```
To prevent razor compilation problems.

Basic usage
```csharp
var email = await Email
    .From("john@email.com")
    .To("bob@email.com", "bob")
    .Subject("hows it going bob")
    .Body("yo bob, long time no see!")
    .SendAsync();
```