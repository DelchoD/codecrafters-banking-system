
namespace BankingManagementSystem.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using Core.Services.Contracts;


    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAccountService accountService;

        public AccountController(IAccountService _accountService)
        {
            accountService = _accountService;
        }
    }
}
