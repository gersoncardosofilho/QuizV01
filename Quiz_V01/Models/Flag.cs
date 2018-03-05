using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Quiz_V01.Models
{
    [Table("tbFlags")]
    public class Flag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFlag { get; set; }

        [Required, DataType(DataType.Text), MaxLength(50)]
        public string FlagName { get; set; }

        [Required, DataType(DataType.ImageUrl)]
        public string FlagUrl { get; set; }

        [Required]
        public int FlagValue { get; set; }
    }
}