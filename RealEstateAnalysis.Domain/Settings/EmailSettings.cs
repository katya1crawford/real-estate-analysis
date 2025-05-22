namespace RealEstateAnalysis.Domain.Settings
{
    public class EmailSettings
    {
        public bool UseSsl { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool WriteAsFile { get; set; }

        public string FileLocation { get; set; }
    }
}