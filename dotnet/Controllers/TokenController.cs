using Microsoft.AspNetCore.Mvc;
using StreamChat.Clients;

namespace StreamProof.Controllers;

[ApiController]
[Route("[controller]")]
public class TokenController : ControllerBase
{
    private readonly IStreamClientFactory _streamClientFactory;

    public TokenController(IStreamClientFactory streamClientFactory)
    {
        _streamClientFactory = streamClientFactory;
    }

    [HttpGet]
    public string Get(string user)
    {
        return _streamClientFactory
            .GetUserClient()
            .CreateToken(user, DateTimeOffset.UtcNow.AddHours(1));
    }
}