using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace PaymentAPI.Models
{
    public class PaymentDetailDbContext : DbContext
    {

        public PaymentDetailDbContext(DbContextOptions options) : base(options)
        {
      
        }

        public DbSet<PaymentDetail> PaymentDetails { get; set; }    
    }
}
