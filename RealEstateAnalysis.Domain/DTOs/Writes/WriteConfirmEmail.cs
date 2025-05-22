namespace RealEstateAnalysis.Domain.DTOs.Writes
{
    public class WriteConfirmEmail
    {
        public string UserId { get; set; }

        public string Token { get; set; }
    }
}