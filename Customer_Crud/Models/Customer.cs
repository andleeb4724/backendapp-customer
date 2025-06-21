using System.ComponentModel.DataAnnotations;

namespace Customer_Crud.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string LoanStatus { get; set; }
        public int TotalCalls { get; set; }
        public int TotalCallDuration { get; set; }
        public ICollection<CallDetail> CallDetails { get; set; }
    }
}
