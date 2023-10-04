using Azure;
using Azure.AI.OpenAI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SDCL_ChatTool.DAL;
using SDCL_ChatTool.DAL.Models;
using SDCL_ChatTool.DAL.Repositories.Interface;
using SDCL_ChatTool.Models;
using System.Text.Json;

namespace SDCL_ChatTool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public ChatController(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Participant>> GetUnusedParticipants()
        {
            var availableParticipants = await _unitOfWork.Repository<IParticipantRepository>().GetQuery().Where(i => i.IsUsed == false).ToListAsync();
            return availableParticipants;
        }

        public async Task<List<Participant>> GetParticipants()
        {
            var participants = await _unitOfWork.Repository<IParticipantRepository>().GetQuery().ToListAsync();
            return participants;
        }

        // public async void NewMessage(List<ChatLog> messages, int participantId)
        public async Task<IActionResult> NewMessage()
        {
            var participantId = 1;
            var messages = new List<ChatLog>() {
                new ChatLog 
                { 
                    ChatMessage = "what is the capital of bangladesh?",
                    ChatRole = ChatRole.User.ToString(),
                    IsSummary = false,
                    ParticipantInfoId = 1
                }
            };

            var latestUserMessage = messages.LastOrDefault();
            if (latestUserMessage == null)
            {
                // No new message
                return BadRequest("List contains 0 message");
            }

            if (latestUserMessage.Id > 0) return BadRequest("No new message to save"); ; // message already in db

            var newMessageFromUser = new ChatLog
            {
                ChatRole = ChatRole.User.ToString(),
                ChatMessage = latestUserMessage.ChatMessage,
                CreatedAt = DateTime.UtcNow,
                IsSummary = latestUserMessage.IsSummary,
                ParticipantInfoId = latestUserMessage.ParticipantInfoId // participantId, // latestMessage.ParticipantInfoId,
            };
            await _unitOfWork.Repository<IChatLogRepository>().AddAsync(newMessageFromUser);

            var chatCompletionsOptions = new ChatCompletionsOptions();
            foreach (var message in messages)
            {
                chatCompletionsOptions.Messages.Add(new ChatMessage(new ChatRole(message.ChatRole), message.ChatMessage));
            }

            var client = new OpenAIClient(_configuration["ChatGpt:API_KEY"], new OpenAIClientOptions());

            try {
                Response<ChatCompletions> response = await client.GetChatCompletionsAsync(
                    deploymentOrModelName: "gpt-3.5-turbo",
                    chatCompletionsOptions);

                var chatMessage = response.Value.Choices[0].Message;

                var lastReponseFromOpenAI = new ChatLog
                {
                    ChatRole = ChatRole.Assistant.ToString(),
                    ChatMessage = chatMessage.Content,
                    CreatedAt = DateTime.UtcNow,
                    IsSummary = false,
                    ParticipantInfoId = participantId, // latestMessage.ParticipantInfoId,
                };
                // await _unitOfWork.Repository<IChatLogRepository>().AddAsync(lastReponseFromOpenAI);

                var responseDump = new ChatGptResponseDump
                {
                    ChatGptResponseId = response.Value.Id,
                    CompletionTokens = response.Value.Usage.CompletionTokens,
                    PromptTokens = response.Value.Usage.PromptTokens,
                    TotalTokens = response.Value.Usage.TotalTokens,
                    ChatLogId = lastReponseFromOpenAI.Id,
                    CreatedAtChatGptEnd = response.Value.Created.UtcDateTime,
                    ContentJson = JsonSerializer.Serialize(response.Value),
                    ChatLog = lastReponseFromOpenAI
                };
                _unitOfWork.Repository<IChatGptResponseDumpRepository>().Add(responseDump);

                await _unitOfWork.SaveChangesAsync();

                // update the message array
                // messages.LastOrDefault = newMessageFromUser.Id;
                latestUserMessage.Id = newMessageFromUser.Id;
                messages.Add(lastReponseFromOpenAI);
                return Ok(messages);

            }
            catch (Exception ex){
                throw ex;
            }
        }
    }
}
