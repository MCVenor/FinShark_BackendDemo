
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//8762840094
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi



builder.Services.AddOpenApi();
builder.Services.AddControllers();


builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();

//2. In your middleware section
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // Creates the /openapi/v1.json file
}


app.UseHttpsRedirection();

app.MapControllers();

app.Run();
