using Azure.Communication.Email;
using OnatrixWebApi.Services;
using OnatrixWebAPI.MiddleWares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiKeyFilter>();
});
// Lägg till IConfiguration som en service
builder.Services.AddSingleton<IConfiguration>(provider => new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build());

builder.Services.AddScoped<CallbackEmail>();
builder.Services.AddScoped<QuestionEmail>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddConnections();
var connectionString = builder.Configuration.GetValue<string>("Values:CommunicationServices");
builder.Services.AddSingleton(new EmailClient(connectionString));

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

app.Run();
