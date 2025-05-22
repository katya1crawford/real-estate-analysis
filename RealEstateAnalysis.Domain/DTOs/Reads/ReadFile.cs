using System;

namespace RealEstateAnalysis.Domain.DTOs.Reads
{
    public class ReadFile
    {
        public ReadFile()
        {
        }

        public ReadFile(long id, string name, byte[] bytes, string mimeType, DateTime createdDate)
        {
            Id = id;
            Name = name;
            ContentBase64 = bytes != null && bytes.Length > 0 ? Convert.ToBase64String(bytes) : string.Empty;
            MimeType = mimeType;
            CreatedDate = createdDate;
        }

        public long Id { get; }

        public string Name { get; }

        public string ContentBase64 { get; }

        public string MimeType { get; }

        public DateTime CreatedDate { get; }
    }
}