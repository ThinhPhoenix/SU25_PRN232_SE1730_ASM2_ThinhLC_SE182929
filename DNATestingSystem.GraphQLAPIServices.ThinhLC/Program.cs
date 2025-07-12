using DNATestingSystem.GraphQLAPIServices.ThinhLC.GraphQLs;
using DNATestingSystem.Repository.ThinhLC.Models;
using DNATestingSystem.Services.ThinhLC;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGraphQLServer().AddQueryType<Query>().AddMutationType<Mutation>().BindRuntimeType<DateTime, DateTimeType>();
//builder.Services.AddGraphQLServer().BindRuntimeType<DateTime, DateTimeType>();

// builder.Services.AddScoped<ISampleThinhLCService, SampleThinhLCService>();
// builder.Services.AddScoped<SystemUserAccountService>();
// builder.Services.AddScoped<ISampleTypeThinhLCService, SampleTypeThinhLCService>();
// builder.Services.AddScoped<IAppointmentsTienDMService, AppointmentsTienDMService>();
// builder.Services.AddScoped<IProfileThinhLCService, ProfileThinhLCService>();
builder.Services.AddScoped<IServiceProviders, ServiceProviders>();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseRouting().UseEndpoints(endpoints => { endpoints.MapGraphQL(); });

app.Run();
