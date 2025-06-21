using Customer_Crud.Data;
using Customer_Crud.DTOs;
using Customer_Crud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Customer_Crud.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _context.Customers.Include(a => a.CallDetails).ToListAsync();
            var customerDtos = customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Mobile = c.Mobile,
                LoanStatus = c.LoanStatus,
                TotalCalls = c.TotalCalls,
                TotalCallDuration = c.TotalCallDuration,
                PaymentHistory = c.CallDetails.Where(cd => cd.IsPaying).Select(cd => new PaymentHistoryDto
                {
                    Date = cd.PaymentDate,
                    Amount = cd.PaymentAmount,
                    Mode = cd.PaymentMode
                }).ToList()
            }).ToList();

            return Ok(customerDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _context.Customers.Include(a => a.CallDetails).FirstOrDefaultAsync(b => b.Id == id);
            var dto = new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Mobile = customer.Mobile,
                LoanStatus = customer.LoanStatus,
                TotalCalls = customer.TotalCalls,
                TotalCallDuration = customer.TotalCallDuration,
                PaymentHistory = customer.CallDetails.Where(cd => cd.IsPaying).Select(cd => new PaymentHistoryDto
                {
                    Date = cd.PaymentDate,
                    Amount = cd.PaymentAmount,
                    Mode = cd.PaymentMode
                }).ToList()
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FirstName) ||
                string.IsNullOrWhiteSpace(dto.LastName) ||
                string.IsNullOrWhiteSpace(dto.Mobile) ||
                string.IsNullOrWhiteSpace(dto.LoanStatus))
            {
                return BadRequest("All fields are required.");
            }

            var customer = new Customer
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Mobile = dto.Mobile,
                LoanStatus = dto.LoanStatus,
                TotalCalls = 0,
                TotalCallDuration = 0,
            };

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FirstName) ||
                string.IsNullOrWhiteSpace(dto.LastName) ||
                string.IsNullOrWhiteSpace(dto.Mobile) ||
                string.IsNullOrWhiteSpace(dto.LoanStatus))
            {
                return BadRequest("All fields are required.");
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound();

            customer.FirstName = dto.FirstName;
            customer.LastName = dto.LastName;
            customer.Mobile = dto.Mobile;
            customer.LoanStatus = dto.LoanStatus;
            customer.TotalCalls = dto.TotalCalls;
            customer.TotalCallDuration = dto.TotalCallDuration;

            await _context.SaveChangesAsync();

            return Ok("Customer updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.Include(c => c.CallDetails).FirstOrDefaultAsync(c => c.Id == id);
            if (customer == null)
                return NotFound();

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok("Record Deleted");
        }
    }
}
