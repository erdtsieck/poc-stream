using Microsoft.AspNetCore.Mvc;
using StreamChat.Clients;
using StreamChat.Models;

namespace StreamProof.Controllers;

[ApiController]
[Route("[controller]")]
public class ChannelsController : ControllerBase
{
    private readonly IStreamClientFactory _streamClientFactory;

    public ChannelsController(IStreamClientFactory streamClientFactory)
    {
        _streamClientFactory = streamClientFactory;
    }

    [HttpGet]
    public async Task<List<ChannelGetResponse>> Post(string user)
    {
        var response = await _streamClientFactory
            .GetChannelClient()
            .QueryChannelsAsync(QueryChannelsOptions.Default
                .WithFilter(new Dictionary<string, object>
                {
                    { "members", new Dictionary<string, object> { { "$in", new[] { user } } } },
                }));

        return response.Channels;
    }
}