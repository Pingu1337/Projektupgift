using Microsoft.EntityFrameworkCore;
using UrlShortenerAPI.Services;
using Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UrlContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// GRPC
builder.Services.AddGrpc();

var app = builder.Build();

// Apply Migrations 
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<UrlContext>();
    db.Database.Migrate();
    Console.WriteLine("Migrations Applied");
}

// GRPC
app.MapGrpcService<UrlService>();


string HOSTNAME = app.Environment.IsDevelopment() ? builder.Configuration.GetSection("host_url").Value ?? "https://localhost/" : "https://localhost/";

URLshort shorter = new();

app.MapGet("/{id}", (string id) =>
{
    return Results.Ok(id);
});


app.MapPost("/shorten", async (URL? url, UrlContext db) =>
{
    if (url == null || url.url == null)
    {
        return Results.BadRequest();
    }

    var shortURL = await shorter.shorten(url.url, HOSTNAME, db);
    return Results.Ok(shortURL);

});

app.Run();
