namespace CollegeApp.data
{
    public class Student
    {

        public int Id { get; set; }

        public string Enrollment_no { get; set; }

        public string Name { get; set; }

        public string Fathers_name { get; set; }

        public string Email { get; set; }

        public DateTime Date_of_birth { get; set; }

        public string Gender { get; set; }


        public string Category { get; set; }

        public string Address { get; set; }

        public DateTime Added_On { get; set; }

        public AdmissionDetails AdmissionDetails { get; set; }
    }
}
