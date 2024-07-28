using System.Text.Json.Serialization;
using Domain.Repositories;
using Infrastructure;
using Infrastructure.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder( args );

// Add services to the container.

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITheaterRepository, TheaterRepository>();
builder.Services.AddScoped<IBusinessHoursRepository, BusinessHoursRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<ICompositionRepository, CompositionRepository>();
builder.Services.AddScoped<IPlayRepository, PlayRepository>();

builder.Services.AddDbContext<TheaterDbContext>( options =>
{
    string connectionString = builder.Configuration.GetConnectionString( "Theater" );
    options.UseSqlServer( connectionString, x => x.MigrationsAssembly( "Infrastructure.Migrations" ) );
} );

builder.Services.AddControllers().AddJsonOptions( options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
} );
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
