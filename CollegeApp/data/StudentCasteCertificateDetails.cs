namespace CollegeApp.data
{
    public class StudentCasteCertificateDetails
    {
        public int Id { get; set; }
        public string CasteCertiNo { get; set; }
        public string CasteCertiUrl { get; set; }
        public string CasteCode { get; set; }
        public string StudentName { get; set; }
        public DateTime RecievedOn { get; set; }
        public string RecievedBy { get; set; }

        // Foreign key relationship
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
