using ElevatorSaga;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace ElevatorSagaBlazor;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<ElevatorApp>("#app");
        builder.Services.AddScoped<IElevatorStrategy, ElevatorStrategy>();
        await builder.Build().RunAsync();
    }
}