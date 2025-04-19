using ExpenseTrackerAPI.Database;
using ExpenseTrackerAPI.Interfaces;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Repo;
using ExpenseTrackerAPI.Utilities;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var salt = builder.Configuration["Security:Salt"];

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Expense Tracker API",
        Description = "An ASP.NET Core Web API for Expense Tracker service",
        //TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            //Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            //Url = new Uri("https://example.com/license")
        }
    });
});

//add middlewares or other services here (user authentication etc.)

//DI - allow dependent injection
builder.Services.AddScoped<DatabaseContext>(); 
builder.Services.AddScoped<PasswordHashing>();
builder.Services.AddScoped<UserRepoInterface, UserRepo>();
builder.Services.AddScoped<ExpenseCategoryRepo>();
builder.Services.AddScoped<TransactionsRepo>();
builder.Services.Configure<PasswordSalt>(builder.Configuration.GetSection("Security")); //register usage of salt

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

app.Run();
