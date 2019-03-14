using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tarea5;
using System.Data.Entity.Core.Objects.DataClasses;

namespace Tarea5.Controllers
{
    public class EVENTOesController : Controller
    {
        private TAREA_5Entities db = new TAREA_5Entities();

        // GET: EVENTOes
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Evento_desc" : "";
            ViewBag.LugarSortParm = String.IsNullOrEmpty(sortOrder) ? "Lugar_desc" : "LUGAR";
            ViewBag.DateSortParm = sortOrder == "FECHA" ? "Fecha_desc" : "FECHA";
            var eventos = from s in db.EVENTOS select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                DateTime fecha = Convert.ToDateTime(searchString).Date;

                eventos = eventos.Where(s => s.FECHA==fecha);

            }

            switch (sortOrder)
            {
                case "Evento_desc":
                    eventos = eventos.OrderByDescending(s => s.EVENTO1);
                    break;
                case "Lugar_desc":
                    eventos = eventos.OrderByDescending(s => s.LUGAR);
                    break;
                case "LUGAR":
                    eventos = eventos.OrderBy(s => s.LUGAR);
                    break;
                case "Fecha_desc":
                    eventos = eventos.OrderByDescending(s => s.FECHA);
                    break;
                case "FECHA":
                    eventos = eventos.OrderBy(s => s.FECHA);
                    break;
                default:
                    eventos = eventos.OrderBy(s => s.EVENTO1);
                    break;


            }
            return View(eventos.ToList());
        }

        // GET: EVENTOes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENTO eVENTO = db.EVENTOS.Find(id);
            if (eVENTO == null)
            {
                return HttpNotFound();
            }
            return View(eVENTO);
        }

        // GET: EVENTOes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EVENTOes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CODIGO,EVENTO1,LUGAR,FECHA")] EVENTO eVENTO)
        {
            if (ModelState.IsValid)
            {
                db.EVENTOS.Add(eVENTO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eVENTO);
        }

        // GET: EVENTOes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENTO eVENTO = db.EVENTOS.Find(id);
            if (eVENTO == null)
            {
                return HttpNotFound();
            }
            return View(eVENTO);
        }

        // POST: EVENTOes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CODIGO,EVENTO1,LUGAR,FECHA")] EVENTO eVENTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eVENTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eVENTO);
        }

        // GET: EVENTOes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EVENTO eVENTO = db.EVENTOS.Find(id);
            if (eVENTO == null)
            {
                return HttpNotFound();
            }
            return View(eVENTO);
        }

        // POST: EVENTOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EVENTO eVENTO = db.EVENTOS.Find(id);
            db.EVENTOS.Remove(eVENTO);
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
