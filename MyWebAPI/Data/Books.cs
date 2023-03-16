

using System.ComponentModel.DataAnnotations;

namespace MyWebAPI.Data
{
    //created using the add-migration init
    public class Books
    {

        //validators
        [Required]
       // [EmailAddress]
        //[RegularExpression("a(b|c)")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
