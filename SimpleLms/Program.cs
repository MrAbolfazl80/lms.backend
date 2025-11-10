using API.Extensions;
using Application.Common;
using Infrastructure.Extensions;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using System;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddControllers();
#region AddDb

builder.Services.AddDbContext<LmsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection")));
#endregion

#region AddDI
builder.Services.AddRepositories();
builder.Services.AddApplicationServices();

#endregion
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region ConfigurationSettings

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
#endregion
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
var app = builder.Build();
#region SeedData
using (var scope = app.Services.CreateScope()) {
    var context = scope.ServiceProvider.GetRequiredService<LmsDbContext>();
    context.Database.Migrate();
    await Infrastructure.Persistence.DataSeeder.SeedDataAsync(context);
}
#endregion
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
