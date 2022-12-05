using Microsoft.EntityFrameworkCore;

using Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UrlContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


string HOSTNAME = app.Environment.IsDevelopment() ? builder.Configuration.GetSection("hostname").Value ?? "http://localhost/" : "http://localhost/";

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
