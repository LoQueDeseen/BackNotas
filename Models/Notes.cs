using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackNotas.Models
{
    public class Notes
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Status { get; set; }
        public DateTime Create_at { get; set; }
        public DateTime Update_at { get; set; }

        [ForeignKey("Categories")]
        public int IdCatego {get; set;}
    }
}