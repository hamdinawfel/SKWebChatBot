using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SKWebChatBot.Services;

namespace SKWebChatBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly SemanticKernelService _semanticKernelService;
        public ChatController(SemanticKernelService semanticKernelService)
        {
            _semanticKernelService = semanticKernelService;
        }

        [HttpPost]
        public async Task<IActionResult> GetChatResponseAsync(string userMessage)
        {
            var response = await _semanticKernelService.GetCharResponceAsync(userMessage);
            return Ok(response);
        }
    }
}
