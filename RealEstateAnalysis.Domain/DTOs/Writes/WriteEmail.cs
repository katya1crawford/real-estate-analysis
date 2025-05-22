namespace RealEstateAnalysis.Domain.DTOs.Writes
{
    public class WriteEmail
    {
        public string ToEmail { get; set; }

        public string FromEmail { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}