using Microsoft.AspNetCore.Mvc;

namespace Powerplant.Controllers;

[ApiController]
[Route("/productionplan")]
public class PayloadController : ControllerBase
{

    private readonly ILogger<PayloadController> _logger;

    public PayloadController(ILogger<PayloadController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "GetPayload")]
    public IActionResult Post([FromBody] Payload payload)
    {
        if (payload == null || payload.Fuels == null || payload.Powerplants == null)
            return BadRequest("The payload isn't correct, please give a valid payload");
        var response = new List<Plant>();
        var sum = 0;
        var cpt = 0;
        for(int i = 0; i < payload.Powerplants.Length; i++)
        {
            response.Add(new Plant()
            {
                Name = payload.Powerplants[i].Name,
                P = payload.Powerplants[i].Pmax
            });
            sum += payload.Powerplants[i].Pmax;
            for (int j = (i + 1 + cpt); j < payload.Powerplants.Length; j++)
            {
                if((sum + payload.Powerplants[j].Pmax) <= payload.Load)
                {
                    response.Add(new Plant()
                    {
                        Name = payload.Powerplants[j].Name,
                        P = payload.Powerplants[j].Pmax
                    });
                    sum += payload.Powerplants[j].Pmax;
                }
                if (sum == payload.Load)
                    return Ok(response);
                if (j == (payload.Powerplants.Length - 1))
                {
                    cpt++;
                    j = (i + cpt);
                    response = new List<Plant>();
                    sum = 0;
                    response.Add(new Plant()
                    {
                        Name = payload.Powerplants[i].Name,
                        P = payload.Powerplants[i].Pmax
                    });
                    sum += payload.Powerplants[i].Pmax;
                }
            }
            response = new List<Plant>();
            sum = 0;
            cpt = 0;
        }
        return Ok("No Solution found");
    }
}
