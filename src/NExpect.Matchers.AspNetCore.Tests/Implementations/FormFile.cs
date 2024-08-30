using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NExpect.Matchers.AspNet.Tests.Implementations;

internal class FormFile : IFormFile
{
    public Stream OpenReadStream()
    {
        var result = new MemoryStream();
        Content.Position = 0;
        Content.CopyTo(result);
        Content.Position = 0;
        return result;
    }

    public void CopyTo(Stream target)
    {
        Content.CopyTo(target);
    }

    public Task CopyToAsync(Stream target, CancellationToken cancellationToken = new CancellationToken())
    {
        return Content.CopyToAsync(target);
    }

    public string ContentType { get; } = "application/octet-stream";
    public string ContentDisposition { get; } = "";
    public IHeaderDictionary Headers { get; } = new HeaderDictionary();
    public long Length => Content.Length;
    public string Name { get; set; }
    public string FileName { get; set; }

    public Stream Content { get; set; } = new MemoryStream();
}