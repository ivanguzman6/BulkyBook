using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.Models.ViewModels
{
    public class ApplicationUserVM
    {

        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<SelectListItem> CompanyList { get; set; }
    }
}
