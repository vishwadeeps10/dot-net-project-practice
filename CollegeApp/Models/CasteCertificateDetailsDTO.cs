namespace CollegeApp.Models
{
    public class CasteCertificateDetailsDTO
    {
        public int Id { get; set; }
        public string CasteCertiNo { get; set; }
        public string CasteCertiUrl { get; set; }
        public string CasteCode { get; set; }
        public string? StudentName { get; set; }
        public DateTime RecievedOn { get; set; }
        public string RecievedBy { get; set; }
        public int StudentId { get; set; }

    }
}
