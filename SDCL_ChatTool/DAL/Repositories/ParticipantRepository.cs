using SDCL_ChatTool.DAL.Models;

namespace SDCL_ChatTool.DAL.Repositories.Interface
{
    public class ParticipantRepository : GenericRepository<Participant>, IParticipantRepository
    {
        public ParticipantRepository(ChatDbContext dbContext) : base(dbContext)
        {
        }
    }
}
