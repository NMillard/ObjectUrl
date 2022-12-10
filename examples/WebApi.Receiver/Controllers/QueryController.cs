using Microsoft.AspNetCore.Mvc;

namespace WebApi.Receiver.Controllers;

[ApiController]
[Route("api/query")]
public class QueryController : ControllerBase
{
    private readonly ILogger<QueryController> logger;

    public QueryController(ILogger<QueryController> logger)
        => this.logger = logger;

    [HttpGet]
    public IActionResult Get(decimal amount, string? creditDebit)
    {
        logger.LogInformation("amount={Amount}; creditDebit={CreditDebit}", amount, creditDebit);   
        return Ok();
    }
}