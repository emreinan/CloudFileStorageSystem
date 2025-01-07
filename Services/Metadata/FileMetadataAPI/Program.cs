using FileMetadataAPI;
using FileMetadataAPI.Application;
using FileMetadataAPI.Core.Exceptions.Extensions;
using FileMetadataAPI.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddJwtAuth(builder.Configuration);
builder.Services.AddFileMetadataAppRegisterServices(builder.Configuration);
builder.Services.AddFileMetadataApiPersistenceServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureCustomExceptionMiddleware();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
