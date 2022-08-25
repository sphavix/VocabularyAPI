using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VocabularyAPI.Models
{
    public class Word
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WordId { get; set; }

        [Display(Name = "Word")]
        public string WordName { get; set; }

        [MaxLength(100)]
        [Display(Name = "Meaning 1")]
        public string Definition1 { get; set; }

        [MaxLength(100)]
        [Display(Name = "Meaning 2")]
        public string Definition2 { get; set; }

        [MaxLength(100)]
        [Display(Name = "Meaning 3")]
        public string Definition3 { get; set; }

        [MaxLength(100)]
        public string Sentence { get; set; }
    }
}
