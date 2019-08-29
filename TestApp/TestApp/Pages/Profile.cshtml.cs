using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TestApp.Pages
{
    public class ProfileModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string FirstName { get; set; }

        [BindProperty]
        public string LastName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        public void OnGet()
        {
            //FirstName = "Atanas";
            LastName = "Vasilev";
        }

        public void OnPost()
        {

        }
    }
}