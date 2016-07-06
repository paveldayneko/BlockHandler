using System.Collections.Generic;
using BlocksHandler.Model;

namespace BlocksHandler.Services.Abstraction
{
    public interface IContentBlockService
    {
        IEnumerable<ContentBlockModel> GetContentBlocks(string fileUri);
    }
}
