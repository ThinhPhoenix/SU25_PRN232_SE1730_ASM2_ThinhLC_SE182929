using DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC;
using DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC.GraphQLClients;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// GraphQL configuration with fallback
var graphQLUri = builder.Configuration["GraphQLURI"] ?? "https://localhost:7040/graphql/";
builder.Services.AddScoped<IGraphQLClient>(c => new GraphQLHttpClient(graphQLUri, new NewtonsoftJsonSerializer()));
builder.Services.AddScoped<GraphQLConsumer>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
