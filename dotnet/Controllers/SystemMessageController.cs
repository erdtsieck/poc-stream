using Microsoft.AspNetCore.Mvc;
using StreamChat.Clients;
using StreamChat.Exceptions;
using StreamChat.Models;

namespace StreamProof.Controllers;

[ApiController]
[Route("[controller]")]
public class SystemMessageController : ControllerBase
{
    private readonly IStreamClientFactory _streamClientFactory;

    public SystemMessageController(IStreamClientFactory streamClientFactory)
    {
        _streamClientFactory = streamClientFactory;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Guid channelId, string messageId, string message)
    {
        try
        {
            var request = new MessageRequest
            {
                Silent = true,
                Id = messageId,
                Text = message,
                UserId = "system",
            };
            var channelRequest = new ChannelRequest();
            channelRequest.SetData("latestSystemMessage", messageId);

            await _streamClientFactory
                .GetChannelClient()
                .UpdateAsync("messaging", channelId.ToString(), new ChannelUpdateRequest
            {
                Message = request,
                Data = channelRequest
            });
        }
        catch (StreamChatException exception) when(MessageAlreadyExists(messageId, exception))
        {
            return Ok($"a message with ID {messageId} already exists");
        }

        return Ok();
    }

    private static bool MessageAlreadyExists(string messageId, Exception exception)
    {
        return exception.Message.Contains($"a message with ID {messageId} already exists", StringComparison.CurrentCultureIgnoreCase);
    }
}