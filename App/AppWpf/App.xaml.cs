﻿using AppWpf.Data;
using AppWpf.Model;
using AppWpf.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using Systeme.CarDriver;

namespace AppWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost __Host;
        public static IHost Host => __Host
            ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();


        public static IServiceProvider Services => Host.Services;

        internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
            .AddDatabase(host.Configuration.GetSection("Database"))
            .AddLoggerService(host.Configuration.GetSection(nameof(LoggerConfig)))
            .AddCarDriverService()
            .AddViewModels()
            ;


        protected override async void OnStartup(StartupEventArgs e)
        {
            var host = Host;

            using (var scope = Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<DbInitializer>().InitializeAsync().Wait();
            }

            base.OnStartup(e);
            await host.StartAsync();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using var host = Host;
            base.OnExit(e);
            await host.StopAsync();
        }
    }
}
