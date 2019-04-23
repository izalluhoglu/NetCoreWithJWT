using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace NetCoreWithJWT.Controllers
{
    public class TicketController : Controller
    {
        [Route("api/ticket")]
        [Authorize]
        [HttpGet]
        public IActionResult GetTicket()
        {
            return new OkObjectResult(new { Name = "Fight Club", PurchaseDate = DateTime.Now });
        }
    }
}
