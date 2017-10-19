using Karin.Contexts;
using Karin.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Karin.Controllers
{
    public class SuppliersController : Controller
    {

        private EFContext context = new EFContext();

        // GET: Suppliers

        #region Index
        public ActionResult Index()
        {
            return View(context
                .Suppliers
                .OrderBy(c => c.Name));
        }
        #endregion Index

        #region Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier supplier)
        {
            context.Suppliers.Add(supplier);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        #endregion Create 

        #region Edit

        public ActionResult Edit(long? id)

        {
            if (id == null)
            {
                return new
                HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = context.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                context.Entry(supplier).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }
        #endregion Edit

        #region Details 
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                    HttpStatusCode.BadRequest);
            }
            Supplier supplier = context.Suppliers.Where(f => f.SupplierId ==
                id).Include("Products.Category").First();
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }
        #endregion [ Details ]

        #region Delete

        public ActionResult Delete(long? id)
        {
            if (id == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            Supplier supplier = context.Suppliers.
                Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            Supplier supplier = context.Suppliers.Find(id);
            context.Suppliers.Remove(supplier);
            context.SaveChanges();
            TempData["Message"] = "Supplier"
                + supplier.Name.ToUpper()
                + "was removed"; return RedirectToAction("Index");
        }

        #endregion [ Delete ]

    }
}



