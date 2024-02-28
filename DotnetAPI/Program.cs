var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors((options) =>
{
    options.AddPolicy("DevCors", (corsBuilder) => // for when in development
    {
        corsBuilder.WithOrigins("http://localhost:4200", "http://localhost:3000")
        .AllowAnyMethod() // allow get, post, delete requests
        .AllowAnyHeader() // allow headers
        .AllowCredentials(); // allows cookies, or other things for authentication
    });
    options.AddPolicy("ProdCord", (corsBuilder) => // for website that will be accessing the api
    {
        corsBuilder.WithOrigins("http://myProductionSite.com")
        .AllowAnyMethod() // allow get, post, delete requests
        .AllowAnyHeader() // allow headers
        .AllowCredentials(); // allows cookies, or other things for authentication
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// if in production
if (app.Environment.IsDevelopment())
{
    app.UseCors("DevCors"); // TODO change to prod cors when in production
    app.UseSwagger();
    app.UseSwaggerUI();
}
// if not make the user use your certificate
else
{
    app.UseHttpsRedirection();
}


app.MapControllers();


app.Run();

