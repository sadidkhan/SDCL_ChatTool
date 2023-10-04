namespace SDCL_ChatTool.DAL.Models
{
    public class ChatLog
    {
        public int Id { get; set; }
        public string ChatRole { get; set; }
        public string ChatMessage { get; set; }
        public bool IsSummary { get; set; }
        public DateTime CreatedAt { get; set; }
        public int SessionId { get; set; }
        public int ParticipantInfoId { get; set; }
    }
}
