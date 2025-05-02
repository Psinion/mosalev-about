namespace MOS.WebApi.Extensions;

public static class Uploads
{
    public static string GetUploadsPath(this IWebHostEnvironment webHost)
    {
        return Environment.GetEnvironmentVariable("UPLOADS_PATH") ?? Path.Combine(webHost.WebRootPath, "uploads");
    }
}