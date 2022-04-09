using Mech;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<MechanicDb>(options => options.UseInMemoryDatabase("items"));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mech API",
        Description = "List, choose and add new mechanics here!",
        Version = "v1"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Add
app.MapPost("/mechanic", async (MechanicDb db, Mechanic mechanic) =>
{
    await db.Mechanics.AddAsync(mechanic);
    await db.SaveChangesAsync();
    return Results.Created($"/mechanic/{mechanic.Id}", mechanic);
});

//GetAll
app.MapGet("/mechanics", async (MechanicDb db) => await db.Mechanics.ToListAsync());

//GetById
app.MapGet("/mechanic/{id}", async (MechanicDb db, int id) => await db.Mechanics.FindAsync(id));

app.Run();