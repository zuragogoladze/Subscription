using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TBC.Capital.Dal.Models
{
    public class BaseTranslation
    {
        public int Id { get; set; }

        [ForeignKey("Language")]
        [Column(TypeName ="NVARCHAR")]
        [StringLength(2)]
        public string LanguageCode { get; set; }
        public Language Language { get; set; }

    }
}
