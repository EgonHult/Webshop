using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop.Models
{
    public class FileUploadModel
    {
        string ContentType { get; }
        string ContentDisposition { get; }
        //IHeaderDictionary Headers { get; }
        long Length { get; }
        string Name { get; }
        string FileName { get; }
        //Stream OpenReadStream();
        //void CopyTo(Stream target);
        //Task CopyToAsync(Stream target, CancellationToken cancellationToken = null);
    }
}
