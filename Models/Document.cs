using System;
using System.ComponentModel.DataAnnotations;

namespace DocManagementSystem.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public int Version { get; set; }

        [Required]
        public byte[] FileData { get; set; }

        [Required]
        public string UploadedBy { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
