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
    public class OperacionesController : Controller
    {
        private AdventureWorksLT2017Entities db = new AdventureWorksLT2017Entities();

        // GET: Operaciones
        public ActionResult Index()
        {
            var operacion = db.Operacion.Include(o => o.Clientes).Include(o => o.Empleados).Include(o => o.Vehiculos);
           
            return View(operacion.ToList());
        }

        // GET: Operaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operacion operacion = db.Operacion.Find(id);
            if (operacion == null)
            {
                return HttpNotFound();
            }
            return View(operacion);
        }

        // GET: Operaciones/Create
        public ActionResult Create()
        {
            ViewBag.ID_cliente = new SelectList(db.Clientes, "ID_Cliente", "NIF");
            ViewBag.ID_empleado = new SelectList(db.Empleados, "ID_Empleado", "NIF");
            ViewBag.ID_vehiculo = new SelectList(db.Vehiculos, "ID_Vehiculo", "marca");
            return View();
        }

        // POST: Operaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Operacion,fecha,tipo,ID_empleado,ID_cliente,precio,ID_vehiculo")] Operacion operacion)
        {
            if (ModelState.IsValid)
            {
                db.Operacion.Add(operacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            Vehiculos veh = db.Vehiculos.Find(operacion.ID_vehiculo);
            if(operacion.tipo.Contains("compra"))
            {
                veh.enStock = true;
            }

            if (operacion.tipo.Contains("venta"))
            {
                veh.enStock = false;
            }

            db.Entry(veh).State = EntityState.Modified;
            db.SaveChanges();

            ViewBag.ID_cliente = new SelectList(db.Clientes, "ID_Cliente", "NIF", operacion.ID_cliente);
            ViewBag.ID_empleado = new SelectList(db.Empleados, "ID_Empleado", "NIF", operacion.ID_empleado);
            ViewBag.ID_vehiculo = new SelectList(db.Vehiculos, "ID_Vehiculo", "marca", operacion.ID_vehiculo);
            return View(operacion);
        }

        // GET: Operaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operacion operacion = db.Operacion.Find(id);
            if (operacion == null)
            {
                return HttpNotFound();
            }

            Vehiculos veh = db.Vehiculos.Find(operacion.ID_vehiculo);
            if (operacion.tipo.Contains("compra"))
            {
                veh.enStock = true;
            }

            if (operacion.tipo.Contains("venta"))
            {
                veh.enStock = false;
            }

            db.Entry(veh).State = EntityState.Modified;
            db.SaveChanges();

            ViewBag.ID_cliente = new SelectList(db.Clientes, "ID_Cliente", "NIF", operacion.ID_cliente);
            ViewBag.ID_empleado = new SelectList(db.Empleados, "ID_Empleado", "NIF", operacion.ID_empleado);
            ViewBag.ID_vehiculo = new SelectList(db.Vehiculos, "ID_Vehiculo", "marca", operacion.ID_vehiculo);
            return View(operacion);
        }

        // POST: Operaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Operacion,fecha,tipo,ID_empleado,ID_cliente,precio,ID_vehiculo")] Operacion operacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(operacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_cliente = new SelectList(db.Clientes, "ID_Cliente", "NIF", operacion.ID_cliente);
            ViewBag.ID_empleado = new SelectList(db.Empleados, "ID_Empleado", "NIF", operacion.ID_empleado);
            ViewBag.ID_vehiculo = new SelectList(db.Vehiculos, "ID_Vehiculo", "marca", operacion.ID_vehiculo);
            return View(operacion);
        }

        // GET: Operaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operacion operacion = db.Operacion.Find(id);
            if (operacion == null)
            {
                return HttpNotFound();
            }
            return View(operacion);
        }

        // POST: Operaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Operacion operacion = db.Operacion.Find(id);
            db.Operacion.Remove(operacion);
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
