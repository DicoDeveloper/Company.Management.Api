using Company.Management.Infrastructure.Data.EF;
using Company.Management.Infrastructure.Data.EF.Setup.Installers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("SpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .InstallMediatr()
                .InstallRepositories()
                .InstallProjections()
                .InstallDbContext(builder.Configuration.GetValue<string>("connectionString") ?? "")
                .InstallMongoDb(builder.Configuration.GetValue<string>("mongoConnectionString") ?? "");

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<Context>();
    context.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseCors("SpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
