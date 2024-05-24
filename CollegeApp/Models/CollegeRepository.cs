namespace CollegeApp.Models
{
    public static class CollegeRepository
    {
        public static List<Student> Students { get; set; } = new List<Student>{
                new Student {
                Id = 1,
                StudentName = "Test",
                Email= "test@gmail.com ",
                Address = "Kankarbagh patna"

            },
                new Student {
                Id = 2,
                StudentName = "Rohit",
                Email= "rohit@gmail.com ",
                Address = "Kankarbagh2 patna"

            }

            };
    }
}
