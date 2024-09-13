using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingManagementSystem.Controllers
{ 
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        [HttpGet("all-customers")]
        [Authorize(Roles = "Admin")]  // Only Admins can access this
        public IActionResult GetAllCustomers()
        {
            // Fetch all customers logic
            return Ok();
        }
    }
}