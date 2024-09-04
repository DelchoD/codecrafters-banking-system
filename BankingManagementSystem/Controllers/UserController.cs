
namespace BankingManagementSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Core.Services.Contracts;


    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

    }
}
