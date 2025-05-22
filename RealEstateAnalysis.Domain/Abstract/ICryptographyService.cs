namespace RealEstateAnalysis.Domain.Abstract
{
    public interface ICryptographyService
    {
        string EncryptText(string text);

        string DecryptText(string ecryptedText);
    }
}