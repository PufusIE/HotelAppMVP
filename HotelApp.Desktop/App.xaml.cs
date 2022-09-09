using Microsoft.Extensions.Hosting;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using HotelAppLibrary.Data;
using Microsoft.Extensions.Configuration;
using HotelAppDataBaseAccessLibrary.Databases;

namespace HotelApp.Desktop
{
    public partial class App : Application
    {
       public static IHost? AppHost { get; private set; }
        public App()
        {
            var builder = Host.CreateDefaultBuilder().ConfigureAppConfiguration(x => x.AddJsonFile("appsettings.json"))
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<MainWindow>();
                    services.AddTransient<ConfirmCheckInWindow>();
                    services.AddTransient<ISQLDataAccess, SQLDataAccess>();
                    services.AddTransient<ISQLiteDataAccess, SQLiteDataAccess>();

                    string dbChoice = hostContext.Configuration.GetValue<string>("DatabaseChoice").ToLower();
                    if (dbChoice == "sql")
                    {
                        services.AddTransient<IDatabaseData, SqlData>();
                    }
                    else if (dbChoice == "sqlite")
                    {
                        services.AddTransient<IDatabaseData, SqliteData>();
                    }

                });          

            AppHost = builder.Build();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();
            var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
            startupForm.Show();
            base.OnStartup(e);
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }

    }
}
