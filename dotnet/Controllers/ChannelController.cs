using Microsoft.AspNetCore.Mvc;
using StreamChat.Clients;
using StreamChat.Models;

namespace StreamProof.Controllers;

[ApiController]
[Route("[controller]")]
public class ChannelController : ControllerBase
{
    private readonly IStreamClientFactory _streamClientFactory;

    public ChannelController(IStreamClientFactory streamClientFactory)
    {
        _streamClientFactory = streamClientFactory;
    }

    [HttpPost]
    public async Task Post(Guid channelId, string onderwerp, string createdBy, string group)
    {
        var channelRequest = new ChannelRequest
        {
            CreatedBy = new UserRequest
            {
                Id = createdBy,
                Name = createdBy
            },
            Members = GroupManager.Groups.SingleOrDefault(x => x.Name == group).Users.Select(x => new ChannelMember
            {
                UserId = x.Id.ToString()
            })
        };
        channelRequest.SetData("name", onderwerp);

        await _streamClientFactory
            .GetChannelClient()
            .GetOrCreateAsync("messaging", channelId.ToString(), new ChannelGetRequest
            {
                Data = channelRequest
            });
    }
}