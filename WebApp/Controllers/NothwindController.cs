using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFModule.DAL;
using EFModule.Models;

namespace WebApp.Controllers
{
    public class NothwindController : Controller
    {
        private readonly NorthwindContext dbcontext;

        public NothwindController(NorthwindContext context)
        {
            dbcontext = context;
        }

        [HttpGet]
        public IQueryable<Categories> categories_view()
        {
            IQueryable<Categories> get_data = dbcontext.Categories.Where(
                data => data != null
                );

            return get_data;
        }

    }
}
