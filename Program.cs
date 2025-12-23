using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebWorkshop;
using WebWorkshop.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");


const string fallbackGraphQlUrl = "https://localhost:7237/graphql";

builder.Services.AddScoped(sp =>
{
    var cfg = sp.GetRequiredService<IConfiguration>();
    var url = cfg["Api:GraphQlUrl"];

    return new HttpClient
    {
        BaseAddress = new Uri(string.IsNullOrWhiteSpace(url) ? fallbackGraphQlUrl : url)
    };
});

builder.Services.AddScoped<GraphQLClient>();

await builder.Build().RunAsync();
