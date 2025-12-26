using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

[ApiController]
[Route("gateway")]
public class GatewayInfoController : ControllerBase
{
    private readonly IConfiguration _config;

    public GatewayInfoController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet("routes")]
    public IActionResult GetRoutes()
    {
        var routes = _config.GetSection("ReverseProxy:Routes").GetChildren()
            .Select(r => new
            {
                RouteId = r.Key,
                ClusterId = r.GetValue<string>("ClusterId"),
                Path = r.GetValue<string>("Match:Path")
            });

        return Ok(routes);
    }
}
