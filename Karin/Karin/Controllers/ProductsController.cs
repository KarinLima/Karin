using Karin.Contexts;
using Karin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Karin.Controllers
{
    public class ProductsController : Controller
    {
        private EFContext context = new EFContext();
        //	GET:	Produtos				
        public ActionResult Index()
        {
            var product = context.Products.Include(c => c.Category).
                Include(s => s.Supplier).OrderBy(n => n.Name);
            return View(product);
        }

        // GET: Products/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.
                BadRequest);
            }
            Product product = context.Products.Where(p => p.ProductId ==
            id).Include(c => c.Category).Include(f => f.Supplier).
            First();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(context.Categories.
                OrderBy(b => b.Name), "CategoryId", "Name");
            ViewBag.SupplierId = new SelectList(context.Suppliers.
                OrderBy(b => b.Name), "SupplierId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                context.Products.Add(product);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(product);
            }
        }

        // GET: Products/Edit/5


        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.
                BadRequest);
            }
            Product product = context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategorieId = new SelectList(context.Categories.
            OrderBy(b => b.Name), "CategoriesId", "Name", product.
            CategorieId);
            ViewBag.FabricanteId = new SelectList(context.Suppliers.
            OrderBy(b => b.Name), "SupliersId", "Name", product.
            SupliersId);

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Entry(product).State = EntityState.Modified;
                    context.SaveChanges();

                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch
            {
                return View(product);
            }
        }


        // POST: Products/Delete/5
       
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.
                BadRequest);
            }
            Product product = context.Products.Where(p => p.ProductId ==
            id).Include(c => c.Category).Include(f => f.Supplier).
            First();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [HttpPost]
        public ActionResult Delete(long id)
        {
            try
            {
                Product product = context.Products.Find(id);
                context.Products.Remove(product);
                context.SaveChanges();
                TempData["Message"] = "Produto " + product.Name.ToUpper()
                    + " foi removido";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
