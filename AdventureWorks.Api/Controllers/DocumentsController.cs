using System.Threading.Tasks;
using AdventureWorks.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AdventureWorks.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentsService _documentsService;

        public DocumentsController(IConfiguration configuration, IDocumentsService documentsService)
        {
            _documentsService = documentsService;
        }
        // POST: api/Documents
        [HttpPost]
        public async Task<IActionResult> PostDocument(IFormFile document)
        {
            var documentId = string.Empty;

            if (document != null)
            {
                using var stream = document.OpenReadStream();
                documentId = await _documentsService.Save(stream);
            }

            return CreatedAtAction("PostDocument", new { id = documentId });
        }
    }
}