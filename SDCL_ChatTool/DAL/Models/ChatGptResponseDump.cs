using System.Text.Json.Serialization;

namespace SDCL_ChatTool.DAL.Models
{
    public class ChatGptResponseDump
    {
        public int Id { get; set; }
        public string ChatGptResponseId { get; set; }
        public int TotalTokens { get; set; }
        public int PromptTokens { get; set; }
        public int CompletionTokens { get; set; }
        public string ContentJson { get; set; }
        public DateTime CreatedAtChatGptEnd { get; set; }
        public int ChatLogId { get; set; }
        public virtual ChatLog ChatLog { get; set; }


    }
}
