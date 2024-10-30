var builder = WebApplication.CreateBuilder(args);

// Agregar la política de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3001",
        builder =>
        {
            builder.WithOrigins("http://localhost:3001")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseCors("AllowLocalhost3001");

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
