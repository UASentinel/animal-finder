using AnimalFinderApp.Services;
using AnimalFinderApp.ViewModels;
using AnimalFinderApp.Views;
using Microsoft.Extensions.Logging;

namespace AnimalFinderApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton(Connectivity.Current);
            builder.Services.AddSingleton(Geolocation.Default);
            builder.Services.AddSingleton(Map.Default);

            builder.Services.AddSingleton<AnimalService>();
            builder.Services.AddSingleton<AnimalsViewModel>();
            builder.Services.AddTransient<AnimalDetailsViewModel>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<AnimalDetails>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
