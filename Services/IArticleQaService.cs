using System.Threading.Tasks;

namespace NadsTech.Services
{
    public interface IArticleQaService
    {
        Task<string> AskAsync(int articleId, string question, string? userId = null);
    }
} 