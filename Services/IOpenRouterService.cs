using System.Threading.Tasks;

namespace NadsTech.Services
{
    public interface IOpenRouterService
    {
        Task<string> AskArticleAsync(string question);
    }
} 