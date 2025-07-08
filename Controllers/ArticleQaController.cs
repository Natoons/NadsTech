using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NadsTech.Services;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace NadsTech.Controllers
{
    [ApiController]
    [Route("api/articleqa")]
    public class ArticleQaController : ControllerBase
    {
        private readonly IArticleQaService _qaService;

        public ArticleQaController(IArticleQaService qaService)
        {
            _qaService = qaService;
        }

        [HttpPost("ask")]
        // [Authorize] // Retiré temporairement pour le diagnostic
        public async Task<IActionResult> Ask([FromBody] ArticleQaRequest req)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine($"[DEBUG] userId: {userId}, ArticleId: {req.ArticleId}, Question: {req.Question}");
            if (string.IsNullOrWhiteSpace(req.Question) || req.ArticleId <= 0)
                return BadRequest("Paramètres invalides.");
            var answer = await _qaService.AskAsync(req.ArticleId, req.Question, userId);
            return Ok(new { answer });
        }

        public class ArticleQaRequest
        {
            public int ArticleId { get; set; }
            public string Question { get; set; } = string.Empty;
        }
    }
} 