using System.ComponentModel.DataAnnotations;

namespace VocabularyAPI.Models
{
    public class Word
    {
        [Key]
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
