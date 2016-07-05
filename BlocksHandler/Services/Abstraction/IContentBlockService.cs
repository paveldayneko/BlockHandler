using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BlocksHandler.Model;


    namespace BlocksHandler.Services
{
    public interface IContentBlockService
    {
        List<ContentBlockModel> GetContentBlocks();
       
    }
}
