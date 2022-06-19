using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//CORS stands for Cross Origin Resource Sharing and by default browsers use this to block websites from requesting data unless that website has permission to do so. This code below determines what websites have access to CORS with this API.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {

        policy.WithOrigins("http://localhost:3002").AllowAnyHeader().AllowAnyMethod();

    });

});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add ToDo context service
builder.Services.AddDbContext<ToDoAPI.Models.ToDoContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoDB"));
    }
    );

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

//step 2 -cors
app.UseCors();

app.Run();
