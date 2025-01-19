using StudentManagement.Mappings;
using StudentManagement.Data;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Services;
using StudentManagement.Data.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();


// Register services
builder.Services.AddScoped<IProfessorService, ProfessorService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseService, CourseService>();



// Register AutoMapper with the profile
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Configuración básica de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "StudentManagement API",
        Version = "v1",
        Description = "A simple API for managing students"
    });
});



// Configurar CORS aquí
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder => builder.WithOrigins("http://localhost:5173") 
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

var app = builder.Build();



// Configurar el middleware para la documentación de Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentManagement API v1");
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigins"); 
app.UseAuthorization();
app.MapControllers();

app.Run();
