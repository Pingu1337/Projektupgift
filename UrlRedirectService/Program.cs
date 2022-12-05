using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UrlContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


app.MapGet("/{id}", async (string id, UrlContext db) =>
{
    var UrlObj = await db.URLs.Where(x => x.nanoid == id).FirstOrDefaultAsync();

    if (UrlObj == null || UrlObj.url == null)
    {
        return Results.NotFound();
    }
    return Results.Redirect(UrlObj.url);
});

app.Run();
