using System;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using DocManagementSystem.Data;
using DocManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DocManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class DocumentsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public DocumentsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "username")?.Value;
            if (user == null) return Unauthorized();

            var file = Request.Form.Files.FirstOrDefault();
            if (file == null || file.Length == 0) return BadRequest("No file uploaded");

            byte[] fileData;
            using (var ms = new System.IO.MemoryStream())
            {
                await file.CopyToAsync(ms);
                fileData = ms.ToArray();
            }

            var latestVersion = await _db.Documents
                .Where(d => d.FileName == file.FileName && d.UploadedBy == user)
                .OrderByDescending(d => d.Version)
                .Select(d => d.Version)
                .FirstOrDefaultAsync();

            var newDoc = new Models.Document
            {
                FileName = file.FileName,
                Version = latestVersion + 1,
                FileData = fileData,
                UploadedBy = user
            };

            _db.Documents.Add(newDoc);
            await _db.SaveChangesAsync();

            return Ok(new { message = "File uploaded successfully", version = newDoc.Version });
        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> Download(string fileName, [FromQuery] int? revision)
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "username")?.Value;
            if (user == null) return Unauthorized();

            var query = _db.Documents.Where(d => d.FileName == fileName && d.UploadedBy == user);

            Models.Document doc;
            if (revision.HasValue)
            {
                doc = await query.FirstOrDefaultAsync(d => d.Version == revision.Value);
            }
            else
            {
                doc = await query.OrderByDescending(d => d.Version).FirstOrDefaultAsync();
            }

            if (doc == null) return NotFound("File not found");

            return File(doc.FileData, "application/octet-stream", fileName);
        }
    }
}
