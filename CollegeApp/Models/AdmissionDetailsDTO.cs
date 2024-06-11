namespace CollegeApp.Models
{
    public class AdmissionDetailsDTO
    {
        public int Id { get; set; }


        public int Student_ID { get; set; }

        public int Class_ID { get; set; }

        public int? Previous_Class_ID { get; set; }

        public int? Annual_Family_Income { get; set; }
        public int? Cast_Certificate_ID { get; set; }

        public DateTime Added_On { get; set; } = DateTime.Now;

        public string? Added_By { get; set; } = "Admin";
    }
}
