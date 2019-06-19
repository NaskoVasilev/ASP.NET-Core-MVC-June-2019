namespace FDMC.ViewModels
{
	public class CatViewModel
	{
		public CatViewModel()
		{
		}

		public CatViewModel(string name, int age, string breed, string imageUrl)
		{
			Name = name;
			Age = age;
			Breed = breed;
			ImageUrl = imageUrl;
		}

		public string Name { get; set; }

		public int Age { get; set; }

		public string Breed { get; set; }

		public string ImageUrl { get; set; }
	}
}
