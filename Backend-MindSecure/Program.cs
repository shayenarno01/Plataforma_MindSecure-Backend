global using Backend_MindSecure.Models;
using Backend_MindSecure.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<dbplataformasaludtdsContext>();
builder.Services.AddSignalR();

builder.Services.AddCors(options => options.AddPolicy("AllowWebApp",
    builder => builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .WithOrigins("http://localhost:4200")
                       /*.AllowCredentials()*/));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowWebApp");

//app.UseCors(options =>
//options.WithOrigins("http://localhost:4200/")
//.AllowAnyMethod()
//.AllowAnyHeader()
//);



//app.UseCors(builder =>
//{
//    builder
//            .AllowAnyHeader()
//            .AllowAnyMethod()
//            .AllowCredentials()
//            .WithOrigins("https://localhost:44344/", "http://localhost:4200/");
//});


app.UseHttpsRedirection();

app.UseAuthorization();
 
app.MapControllers();

app.UseCors("SignalRClientPolicy");



app.MapHub<ChatHub>("/hubs/chat");



app.Run();
