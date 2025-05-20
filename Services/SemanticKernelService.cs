using Microsoft.SemanticKernel;

namespace SKWebChatBot.Services
{
    public class SemanticKernelService
    {
        private readonly Kernel _kernel;
        public SemanticKernelService(IConfiguration configuration)
        {
            var deploymentName = configuration["SemanticKernel.deploymentName"];
            var endpoint = configuration["SemanticKernel.endpoint"];
            var apiKey = configuration["SemanticKernel.apiKey"];

            var builder = Kernel.CreateBuilder();

            builder.AddAzureOpenAIChatCompletion(
                deploymentName : deploymentName,
                endpoint : endpoint,
                apiKey : apiKey);

            _kernel = builder.Build();
        }


        public async Task<string> GetCharResponceAsync(string userMessage)
        {
            var result = await _kernel.InvokePromptAsync(userMessage);
            return result.ToString();
        }
    }
}
