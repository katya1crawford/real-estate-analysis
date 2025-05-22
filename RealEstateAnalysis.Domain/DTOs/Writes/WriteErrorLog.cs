namespace RealEstateAnalysis.Domain.DTOs.Writes
{
    public class WriteErrorLog
    {
        public string ClassName { get; set; }

        public string MethodName { get; set; }

        public string Values { get; set; }

        public string Error { get; set; }
    }
}