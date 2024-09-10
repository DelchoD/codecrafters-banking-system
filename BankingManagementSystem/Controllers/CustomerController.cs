using BankingManagementSystem.Core.Models.Customer;
using BankingManagementSystem.Core.Models.User;
using BankingManagementSystem.Core.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly CustomerService _customerService;

    public CustomerController(CustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerFormDTO>> GetCustomerById(int id)
    {
        var customer = await _customerService.GetCustomerDTOById(id);
        if (customer == null) return NotFound();
        return Ok(customer);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerAllDTO>>> GetAllCustomers()
    {
        var customers = await _customerService.GetAllCustomers();
        return Ok(customers);
    }

    [HttpPost]
    public async Task<ActionResult> CreateCustomer([FromBody] CustomerFormDTO dto)
    {
        var customerAllDTO = await _customerService.RegisterCustomer(dto);
        return CreatedAtAction(nameof(GetCustomerById), new { id = customerAllDTO.Id}, dto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCustomer(int id, [FromBody] CustomerUpdateDTO dto)
    {
        await _customerService.UpdateCustomerProfile(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCustomer(int id)
    {
        await _customerService.DeleteCustomer(id);
        return NoContent();
    }
}
