using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeApp.data
{
    public class AdmissionDetails
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("Student")]
        public int Student_ID { get; set; }

        public Student Student { get; set; }

        public int Class_ID { get; set; }

        public int? Previous_Class_ID { get; set; }

        public int? Annual_Family_Income { get; set; }
        public int? Cast_Certificate_ID { get; set; }

        public DateTime Added_On { get; set; }

        public string Added_By { get; set; }
    }
}
