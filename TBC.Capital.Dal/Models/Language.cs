using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TBC.Capital.Dal.Models
{
    public class Language
    {
        [Key, Column(TypeName ="NVARCHAR"),Required]
        [StringLength(2)]
        public string Code { get; set;}

        [Required]
        [Index]
        [StringLength(20)]
        public string Name { get; set; }
    }
}
