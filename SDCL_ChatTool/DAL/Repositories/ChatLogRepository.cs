using SDCL_ChatTool.DAL.Models;

namespace SDCL_ChatTool.DAL.Repositories.Interface
{
    public class ChatLogRepository : GenericRepository<ChatLog>, IChatLogRepository
    {
        public ChatLogRepository(ChatDbContext dbContext) : base(dbContext)
        {
        }
    }
}
