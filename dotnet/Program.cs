using StreamChat.Clients;
using StreamChat.Models;
using StreamProof;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(x =>
{
    x.AddPolicy("default", c =>
    {
        c
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});
builder.Services.AddTransient<IStreamClientFactory>(sp => new StreamClientFactory(
    builder.Configuration.GetSection("ApiKey").Value,
    builder.Configuration.GetSection("ApiSecret").Value));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors("default");

app.UseAuthorization();

app.MapControllers();

app.Services.GetRequiredService<IStreamClientFactory>().GetUserClient().UpsertManyAsync(GroupManager.Users.Select(x =>
    new UserRequest
    {
        Id = x.Id.ToString(),
        Name = x.Name
    })).GetAwaiter().GetResult();

app.Run();
