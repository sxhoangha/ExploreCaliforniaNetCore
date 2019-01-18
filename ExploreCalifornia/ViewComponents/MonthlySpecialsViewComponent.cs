using ExploreCalifornia.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia.ViewComponents
{
    public class MonthlySpecialsViewComponent : ViewComponent
    {
        private readonly BlogDataContext _db;

        public MonthlySpecialsViewComponent(BlogDataContext db)
        {
            _db=db;
        }
        public IViewComponentResult Invoke()
        {
            var special = _db.Specials.ToArray();
            return View(special);
        }
    }
}
