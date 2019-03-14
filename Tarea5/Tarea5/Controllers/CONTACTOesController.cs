using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tarea5;

namespace Tarea5.Controllers
{
    public class CONTACTOesController : Controller
    {
        private TAREA_5Entities db = new TAREA_5Entities();

        // GET: CONTACTOes
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Nombre_desc" : "";
            var contactos = from s in db.CONTACTOes select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                contactos = contactos.Where(s => s.NOMBRE.Contains(searchString));
            }



            switch (sortOrder)
            {
                case "Nombre_desc":
                    contactos = contactos.OrderByDescending(s => s.NOMBRE);
                    break;
                default:
                    contactos = contactos.OrderBy(s => s.NOMBRE);
                    break;


            }
            return View(contactos.ToList());
        }

        // GET: CONTACTOes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTACTO cONTACTO = db.CONTACTOes.Find(id);
            if (cONTACTO == null)
            {
                return HttpNotFound();
            }
            return View(cONTACTO);
        }

        // GET: CONTACTOes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CONTACTOes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CODIGO,NOMBRE,TELEFONO,DIRECCION")] CONTACTO cONTACTO)
        {
            if (ModelState.IsValid)
            {
                db.CONTACTOes.Add(cONTACTO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cONTACTO);
        }

        // GET: CONTACTOes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTACTO cONTACTO = db.CONTACTOes.Find(id);
            if (cONTACTO == null)
            {
                return HttpNotFound();
            }
            return View(cONTACTO);
        }

        // POST: CONTACTOes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CODIGO,NOMBRE,TELEFONO,DIRECCION")] CONTACTO cONTACTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cONTACTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cONTACTO);
        }

        // GET: CONTACTOes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTACTO cONTACTO = db.CONTACTOes.Find(id);
            if (cONTACTO == null)
            {
                return HttpNotFound();
            }
            return View(cONTACTO);
        }

        // POST: CONTACTOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CONTACTO cONTACTO = db.CONTACTOes.Find(id);
            db.CONTACTOes.Remove(cONTACTO);
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
