using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using SKWebChatBot.Configuration;

namespace SKWebChatBot.Services
{
    public class SemanticKernelService
    {
        private readonly Kernel _kernel;
        private readonly IOptions<SemanticKernelSettings> _settings;
        public SemanticKernelService(IOptions<SemanticKernelSettings> settings)
        {
            _settings = settings;
            var deploymentName = _settings.Value.DeploymentName;
            var endpoint = _settings.Value.Endpoint;
            var apiKey = _settings.Value.ApiKey;

            var builder = Kernel.CreateBuilder();

            builder.AddAzureOpenAIChatCompletion(
                deploymentName : deploymentName,
                endpoint : endpoint,
                apiKey : apiKey);

            _kernel = builder.Build();
        }


        public async Task<string> GetChatResponseAsync(string userMessage)
        {
            var result = await _kernel.InvokePromptAsync(userMessage);
            return result.ToString();
        }
    }
}
