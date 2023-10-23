using System.ComponentModel.DataAnnotations;

namespace Template.RazorWebApp.Models
{
    public class Student
    {
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required, StringLength(10)]
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }


    }
}
