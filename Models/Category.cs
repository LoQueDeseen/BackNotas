
using System.ComponentModel.DataAnnotations.Schema;

namespace BackNotas.Models{

    public class Category {

        public int Id {get; set;}
        public string Name {get; set;}

        public string Status {get; set;} 

        public DateTime? Create_at   {get; set;}
        public DateTime? Update_at   {get; set;}

        [ForeignKey("Users")]
        public int UserId {get; set;}


    }
}