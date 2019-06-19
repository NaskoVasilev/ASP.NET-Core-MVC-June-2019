using System.ComponentModel.DataAnnotations;

namespace FDMC.Data.Models
{
    public class Cat
    {
        public Cat()
        {
        }

        public Cat(string name, int age, string breed, string imageUrl)
        {
            Name = name;
            Age = age;
            Breed = breed;
            ImageUrl = imageUrl;
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0, 20)]
        public int Age { get; set; }

        public string Breed { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
