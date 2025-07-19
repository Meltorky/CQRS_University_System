using CQRS_University_System.API.Middlewares;
using CQRS_University_System.Application;
using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.Interfaces;
using CQRS_University_System.Infrastructure.Data;
using CQRS_University_System.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add DataBase Connection
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly);
});
builder.Services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyReference).Assembly); // Use a marker interface if needed


// Inject System Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// registering the Global Exception Handling Middleware.
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
