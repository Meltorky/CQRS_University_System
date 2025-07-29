using CQRS_University_System.API.ActionFilters;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Configure Swagger to use XML comments
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(System.IO.Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.EnableAnnotations();


    // Configure Swagger to use JWT tokens
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "CQRS University System", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter JWT token like: Bearer {your token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


// Add DataBase Connection
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));


// Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(o =>
{
    o.Password.RequireDigit = true;
    o.Password.RequireLowercase = true;
    o.Password.RequireUppercase = true;
    o.Password.RequireNonAlphanumeric = true;
    o.Password.RequiredLength = 8;
}).AddEntityFrameworkStores<AppDbContext>();


builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly);
});
builder.Services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyReference).Assembly); // Use a marker interface if needed


// Add JWT authentication in program.cs
builder.Services.AddJwtAuthentication(builder.Configuration);


// Inject Identity Services
builder.Services.AddScoped<ITokenService, TokenService>();

// Inject System Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();


// Add Action Filter that calculate the time excution
builder.Services.AddScoped<ExecutionTimeFilter>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseSwagger();
//app.UseSwaggerUI(options =>
//{
//    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Production v1");
//    options.RoutePrefix = string.Empty;
//});

// registering the Global Exception Handling Middleware.
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Seed Identity Default Roles and Users
await app.Services.SeedIdentityAsync();

app.Run();