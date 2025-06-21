using Customer_Crud.Data;
using Customer_Crud.DTOs;
using Customer_Crud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customer_Crud.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallDetailsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CallDetailsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var callDetails = await _context.CallDetails
                .Include(cd => cd.Customer)
                .ToListAsync();

            var result = callDetails.Select(cd => new CallDetailDto
            {
                Id = cd.Id,
                CustomerId = cd.CustomerId,
                CallStart = cd.CallStart,
                CallEnd = cd.CallEnd,
                CallDuration = cd.CallDuration,
                IsPaying = cd.IsPaying,
                PaymentDate = cd.PaymentDate,
                PaymentAmount = cd.PaymentAmount,
                PaymentMode = cd.PaymentMode
            }).ToList();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCallDetailDto dto)
        {
            if (dto == null || dto.CustomerId <= 0)
                return BadRequest("Invalid call detail input.");

            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == dto.CustomerId);
            if (customer == null)
                return NotFound($"Customer with ID {dto.CustomerId} not found.");

            var callDetail = new CallDetail
            {
                CustomerId = dto.CustomerId,
                CallStart = dto.CallStart,
                CallEnd = dto.CallEnd,
                CallDuration = dto.CallDuration,
                IsPaying = dto.IsPaying,
                PaymentDate = dto.IsPaying ? dto.PaymentDate : null,
                PaymentAmount = dto.IsPaying ? dto.PaymentAmount : null,
                PaymentMode = dto.IsPaying ? dto.PaymentMode : null
            };

            await _context.CallDetails.AddAsync(callDetail);

            customer.TotalCalls += 1;
            customer.TotalCallDuration += dto.CallDuration;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Call detail created and customer updated successfully." });
        }
    }
}
