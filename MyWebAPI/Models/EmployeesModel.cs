

using System.ComponentModel.DataAnnotations;

namespace MyWebAPI.Models
{
    //connected with the db , and manually created table
    public class EmployeesModel
    {
        public int emp_id { get; set; }
        [Required]
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string department { get; set; }
        public decimal salary { get; set; }
        public DateTime hire_date { get; set; }
    }
}
