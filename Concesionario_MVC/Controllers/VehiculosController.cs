using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Concesionario_MVC;

namespace Concesionario_MVC.Controllers
{
    public class VehiculosController : Controller
    {
        private AdventureWorksLT2017Entities db = new AdventureWorksLT2017Entities();

        // GET: Vehiculos
        public ActionResult Index(bool? disponible, string searchMarca, string searchModelo, long? km)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Si", Value = "true"});
            items.Add(new SelectListItem { Text = "No", Value = "false" });
            ViewBag.disponible = items;


            List<SelectListItem> listaKilometros = new List<SelectListItem>();
            for (int i = 5000; i <= 250000; i +=10)
            {
                listaKilometros.Add(new SelectListItem
                {
                    Text = "hasta " + i,
                    Value = i.ToString(),
                    
                });
                i *= 2;
            }
            ViewBag.km = listaKilometros;

            var vehiculos = from v in db.Vehiculos
                            select v;

            if (!string.IsNullOrEmpty(disponible.ToString()))
            {
                vehiculos = vehiculos.Where(x => x.enStock == disponible);
            }

            if (!String.IsNullOrEmpty(searchMarca))
            {
                vehiculos = vehiculos.Where(s => s.marca.Contains(searchMarca));
            }

            if (!String.IsNullOrEmpty(searchModelo))
            {
                vehiculos = vehiculos.Where(s => s.marca.Contains(searchModelo));
            }

            if (!string.IsNullOrEmpty(km.ToString()))
            {
                vehiculos = vehiculos.Where(x => x.kilometros <= km);
            }

            return View(vehiculos);
        }

        // GET: Vehiculos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculos vehiculos = db.Vehiculos.Find(id);
            if (vehiculos == null)
            {
                return HttpNotFound();
            }
            return View(vehiculos);
        }

        // GET: Vehiculos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vehiculos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Vehiculo,marca,numeroPuertas,color,kilometros,tipoVehiculo,garantia,enStock,fotografia,modelo")] Vehiculos vehiculos)
        {
            if (ModelState.IsValid)
            {
                db.Vehiculos.Add(vehiculos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehiculos);
        }

        // GET: Vehiculos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculos vehiculos = db.Vehiculos.Find(id);
            if (vehiculos == null)
            {
                return HttpNotFound();
            }
            return View(vehiculos);
        }

        // POST: Vehiculos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Vehiculo,marca,numeroPuertas,color,kilometros,tipoVehiculo,garantia,enStock,fotografia,modelo")] Vehiculos vehiculos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehiculos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehiculos);
        }

        // GET: Vehiculos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehiculos vehiculos = db.Vehiculos.Find(id);
            if (vehiculos == null)
            {
                return HttpNotFound();
            }
            return View(vehiculos);
        }

        // POST: Vehiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehiculos vehiculos = db.Vehiculos.Find(id);
            db.Vehiculos.Remove(vehiculos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
