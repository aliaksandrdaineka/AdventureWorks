using System.IO;
using System.Threading.Tasks;

namespace AdventureWorks.Services
{
    public interface IDocumentsService
    {
        Task<string> Save(Stream stream, string filename, string metadata);
    }
}
