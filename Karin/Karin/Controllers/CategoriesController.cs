using Karin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Karin.Controllers
{
    public class CategoriesController : Controller
    {
        #region list
        private static IList<Category> categories = new List<Category>()

        {

new Category() { CategoryId = 1,Name = "Minnie"},
new Category() { CategoryId = 2,Name = "Mickey"},
new Category() { CategoryId = 3,Name = "Pluto"},
new Category() { CategoryId = 4,Name = "Donald"},
new Category() { CategoryId = 5,Name = "Margarida"}

};
        #endregion list
        #region index
        // GET: Categorias
        public ActionResult Index()
        {
            return View(categories.OrderBy(c => c.Name));
        }
        #endregion index
        #region Create 
        public ActionResult Create()

        {

            return View();

        }


        [HttpPost]

        [ValidateAntiForgeryToken]

        public ActionResult Create(Category category)

        {

            categories.Add(category);
            category.CategoryId =
            categories.Select(m => m.CategoryId).Max() + 1;
            return RedirectToAction("Index");

        }
        #endregion Create 
        #region Edit

        public ActionResult Edit(long id)

        {
            return View(categories.Where( m => m.CategoryId == id).First());
        }


        [HttpPost]

        [ValidateAntiForgeryToken]

        public ActionResult Edit(Category category)

        {
            categories.Remove(categories.Where(c => c.CategoryId == category.CategoryId).First());
            categories.Add(category);
            return RedirectToAction("Index");

        }

        #endregion Edit 
        #region Details
        public ActionResult Details(long id)

        {

            return View(categories.Where(

            m => m.CategoryId == id).First());

        }
        #endregion Details 
        #region Delete 
        public ActionResult Delete(long id)

        {

            return View(categories.Where(m => m.CategoryId == id).First());

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Delete(Category category)

        {
            categories.Remove(categories.Where(c => c.CategoryId == category.CategoryId).First());
            return RedirectToAction("Index");
        }

        #endregion Delete 

    }
}