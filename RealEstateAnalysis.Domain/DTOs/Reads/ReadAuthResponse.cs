namespace RealEstateAnalysis.Domain.DTOs.Reads
{
    public class ReadAuthResponse
    {
        public ReadAuthResponse(string token, string refreshToken)
        {
            Token = token;
            RefreshToken = refreshToken;
        }

        public string Token { get; }

        public string RefreshToken { get; }
    }
}