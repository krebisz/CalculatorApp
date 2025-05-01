using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;



namespace CalculatorApp;

///<summary>
///Registration for Dependency Injection
///</summary>
public partial class App : Application
{
    private IHost _host;

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                //Operation Registration
                services.AddTransient<IOperation, AdditionOperation>();
                services.AddTransient<IOperation, SubtractionOperation>();
                services.AddTransient<IOperation, MultiplicationOperation>();
                services.AddTransient<IOperation, DivisionOperation>();


                services.AddSingleton<Calculator>();

                //MainWindow Registration
                services.AddTransient<MainWindow>();
            })
            .Build();

        await _host.StartAsync();

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _host.StopAsync(TimeSpan.FromSeconds(5));
        _host.Dispose();
        base.OnExit(e);
    }
}