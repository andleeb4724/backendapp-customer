namespace Customer_Crud.DTOs
{
    public class CallDetailDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime CallStart { get; set; }
        public DateTime CallEnd { get; set; }
        public int CallDuration { get; set; }
        public bool IsPaying { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentAmount { get; set; }
        public string? PaymentMode { get; set; }
    }
}
