using Model;
public class URLshort
{
    internal async Task<string> shorten(string fullURL, UrlContext db)
    {
        var nanoid = await Nanoid.Nanoid.GenerateAsync(size: 7);
        var url = new URL()
        {
            nanoid = nanoid,
            url = fullURL
        };

        await db.URLs.AddAsync(url);
        await db.SaveChangesAsync();

        var shortURL = buildUrl(nanoid);
        return shortURL;
    }

    internal string buildUrl(string id)
    {
        string shortURL = "http://localhost/" + id;
        return shortURL;
    }
}