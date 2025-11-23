using APIProject.Models;
using APIProject.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

//add Database Configuration

var connectionString = builder.Configuration.GetConnectionString("DefaultDatabase"); //connections string name of database
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString)); // add service of database


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
//Add authenications jwt in the api
 {
    options.AddSecurityDefinition(name:"Bearer",securityScheme:new OpenApiSecurityScheme

    {
        Name = "Authorized",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWt",
        In = ParameterLocation.Header,
        Description ="Enter Your JWT Key "

    });
});
builder.Services.AddEndpointsApiExplorer();

//to add core  add service at frist
builder.Services.AddCors();

// Add Service of Genres 
builder.Services.AddTransient<IGenresService , GenresServices>();

//add services of movies
builder.Services.AddTransient<IMovieServices,MovieServices>();

//AddAutoMapper

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// add cores

app.UseCors(c=>c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();

app.MapControllers();

app.Run();





