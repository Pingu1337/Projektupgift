using Microsoft.EntityFrameworkCore;
using Grpc.Net.Client;
using UrlRedirectService.Protos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UrlContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddGrpc();

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


//GRPC
app.MapGet("/test/{id}", async (string id) =>
{
    // Docker Network
    var channel = GrpcChannel.ForAddress("http://urlshortenerapi");

    // var channel = GrpcChannel.ForAddress("https://localhost:52027");
    var client = new GetUrlService.GetUrlServiceClient(channel);

    var urlRequest = new UrlRequest
    {
        Nanoid = id
    };

    var url = await client.GetUrlAsync(urlRequest);

    if (url == null)
    {
        return Results.NotFound("404 not found");
    }
    return Results.Redirect(url.Url);
});

app.Run();
