using PhoneDB;
using PhoneDB.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IPhoneRepository, SQLPhoneRepository>();

builder.Services.AddSingleton<DBConfig>(services =>
{
    var configuration = services.GetRequiredService<IConfiguration>();
    return new DBConfig
    {
        ConnectionString = configuration.GetConnectionString("DefaultConnection")
    };
});

var app = builder.Build();

app.UseCors(x => x
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .SetIsOriginAllowed(origin => true)
                  .AllowCredentials());

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
