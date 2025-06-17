using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace UploadFileApi.Entities
{
    public class ImageEntity
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public byte[] Data { get; set; }

    }
}
