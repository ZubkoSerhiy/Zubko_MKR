using System.ComponentModel.DataAnnotations;

namespace LR6.Models
{
    public class LabViewModel
    {
        [Required]
        public IFormFile InputFile { get; set; }
        public string InputContent { get; set; }
        public string OutputContent { get; set; }
        public string Description { get; set; }
        public string TaskNumber { get; set; }
        public string TaskVariant { get; set; }
        public string TaskDescription { get; set; }
        public string InputDescription { get; set; }
        public string OutputDescription { get; set; }
    }
}