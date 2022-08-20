using BackgroundWorker;
using Coravel;
using Serilog;
using Serilog.Formatting.Json;

//Simple Serilog logger
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(new JsonFormatter(), "logs/log.txt", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information, rollingInterval: RollingInterval.Day) //Each day serilog will create a new log file for a new day
    .WriteTo.File("logs/warninglog.txt", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Warning)
    .CreateLogger();

try
{
    Log.Information("Starting our service...");

    IHost host = Host.CreateDefaultBuilder(args)
    .UseSerilog()
    .ConfigureServices(services =>
    {
        //Define the smtp for FluentEmail. The environmental variables needs to be defined first
        var smtpHost = Environment.GetEnvironmentVariable("SMTP_HOST");
        var smtpUser = Environment.GetEnvironmentVariable("SMTP_USER");
        var smtpPass = Environment.GetEnvironmentVariable("SMTP_PASS");

        //Configure FluentEmail
        services
            .AddFluentEmail("sender@sender.com") //Default from email
            .AddRazorRenderer()
            .AddSmtpSender(smtpHost, 587, smtpUser, smtpPass);

        services.AddHostedService<Worker>();

        //Here we configure scheduler
        services.AddScheduler();
        services.AddTransient<ProcessOrder>();
    })
    .Build();

    //Configure Coravel scheduler
    host.Services.UseScheduler(schedule =>
    {
        //We define the jobSchedule for a certain ProcessOrder
        var jobSchedule = schedule.Schedule<ProcessOrder>();
        //Here we make a schedule: every weekday (so not Saturday and Sunday) at 9 in the morning.
        //jobSchedule.DailyAtHour(9).Weekday();
        //Here is every two seconds
        //PreventOverlapping means that process will wait for the previous process to complete until the next process is executed.
        jobSchedule.EverySeconds(30);//.PreventOverlapping("ProcessOrderJob");
    });

    await host.RunAsync();
}
catch (Exception exception)
{
    Log.Fatal(exception, "Exception in application");
}
finally
{
    Log.Information("Exiting service");
    Log.CloseAndFlush();
}
