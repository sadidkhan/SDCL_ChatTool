using SDCL_ChatTool.DAL.Models;

namespace SDCL_ChatTool.DAL.Repositories.Interface
{
    public class ChatGptResponseDumpRepository : GenericRepository<ChatGptResponseDump>, IChatGptResponseDumpRepository
    {
        public ChatGptResponseDumpRepository(ChatDbContext dbContext) : base(dbContext)
        {
        }
    }
}
