using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer_Crud.Models
{
    public class CallDetail
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public DateTime CallStart { get; set; }
        public DateTime CallEnd { get; set; }
        public int CallDuration { get; set; }
        public bool IsPaying { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentAmount { get; set; }
        public string? PaymentMode { get; set; }
        public Customer Customer { get; set; }

    }
}
