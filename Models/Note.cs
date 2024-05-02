using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackNotas.Models
{
    public class Note
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public string Status { get; set; }
        public DateTime Create_at { get; set; }
        public DateTime? Updated_at { get; set; }

        [ForeignKey("Categories")]
        public int IdCategory {get; set;}
    }
}