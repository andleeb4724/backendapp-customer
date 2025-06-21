namespace Customer_Crud.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string LoanStatus { get; set; }
        public int TotalCalls { get; set; }
        public int TotalCallDuration { get; set; }
        public List<PaymentHistoryDto> PaymentHistory { get; set; }
    }
}
