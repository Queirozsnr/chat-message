using Api.Chat.Hubs;

var builder = WebApplication.CreateBuilder(args);

//Add signalR
builder.Services.AddSignalR();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Add User cors
app.UseCors(cors =>
{
    cors.AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials().WithOrigins("http://localhost:8080");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Add Map Hub
app.MapHub<HubProvider>("/Hub");

app.Run();
