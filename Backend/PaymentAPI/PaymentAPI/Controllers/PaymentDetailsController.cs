using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Models;

namespace PaymentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentDetailsController : ControllerBase
    {
        private readonly PaymentDetailDbContext _context;
        public PaymentDetailsController(PaymentDetailDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentDetails()
        {
            var result = await _context.PaymentDetails.ToListAsync();
            if(_context.PaymentDetails == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddPaymentDetail([FromBody] PaymentDetail paymentDetail)
        {
           // paymentDetail.PaymentDetailId = Guid.NewGuid();

            await _context.PaymentDetails.AddAsync(paymentDetail);
            await _context.SaveChangesAsync();

            return Ok(paymentDetail);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentDetail(int id)
        {
            if(_context.PaymentDetails == null)
            {
                return NotFound();
            }
            var paymentDetail = await _context.PaymentDetails.FindAsync(id);

            if(paymentDetail == null)
            {
                return NotFound();
            }
            return Ok(paymentDetail);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaymentDetail([FromRoute] int id, PaymentDetail updatePaymentDetailRequest)
        {
            var result = await _context.PaymentDetails.FindAsync(id);

            if (result == null)
                return NotFound();

            result.CardOwnerName = updatePaymentDetailRequest.CardOwnerName;
            result.CardNumber = updatePaymentDetailRequest.CardNumber;
            result.ExpirationDate = updatePaymentDetailRequest.ExpirationDate;
            result.SecurityCode = updatePaymentDetailRequest.SecurityCode;

            await _context.SaveChangesAsync();

            return Ok(result);
        }

        [HttpDelete("{id}")]        
        public async Task<IActionResult> DeletePaymentDetail(int id)
        {
            var result = await _context.PaymentDetails.FindAsync(id);

            if (result == null)
                return NotFound();

            _context.PaymentDetails.Remove(result);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
