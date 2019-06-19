using System.ComponentModel.DataAnnotations;

namespace FDMC.ViewModels
{
	public class CatCreateInputModel
	{
        [MinLength(3), MaxLength(30)]
        [Required]
        public string Name { get; set; }

        [Range(0, 20)]
        public int Age { get; set; }


        public string Breed { get; set; }

        [Required]
        [RegularExpression(@"http:\/\/.+|https:\/\/.+")]
        public string ImageUrl { get; set; }
    }
}
