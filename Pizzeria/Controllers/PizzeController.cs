using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Pizzeria.Models
{

    public class PizzeController : Controller
    {
        private ModeldbContext db = new ModeldbContext();

        // GET: Pizze
        public ActionResult Index()
        {
            return View(db.Pizze.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizze pizze = db.Pizze.Find(id);
            if (pizze == null)
            {
                return HttpNotFound();
            }
            return View(pizze);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pizze/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Pizza,Nome,Prezzo,Tempo_Consegna,Ingredienti,Immagine")] Pizze pizze, HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Immagini"), fileName);
                    file.SaveAs(path);
                    pizze.Immagine = "/Content/Immagini/" + fileName;
                }
                else
                {
                    pizze.Immagine = "/Content/Immagini/default.jpg";
                }

                if (ModelState.IsValid)
                {
                    db.Pizze.Add(pizze);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                // Gestisci l'eccezione, registrandola o visualizzando un messaggio all'utente
                Console.WriteLine("Errore durante il salvataggio del file: " + ex.Message);
            }

            // Se si arriva a questo punto, significa che c'è un errore nel modello o nel salvataggio
            return View(pizze);
        }




        // GET: Pizze/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizze pizze = db.Pizze.Find(id);
            if (pizze == null)
            {
                return HttpNotFound();
            }
            return View(pizze);
        }

        // POST: Pizze/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Pizza,Nome,Prezzo,Tempo_Consegna,Ingredienti,Immagine")] Pizze pizze)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pizze).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pizze);
        }

        // GET: Pizze/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizze pizze = db.Pizze.Find(id);
            if (pizze == null)
            {
                return HttpNotFound();
            }
            return View(pizze);
        }

        // POST: Pizze/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pizze pizze = db.Pizze.Find(id);
            db.Pizze.Remove(pizze);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Carrello()
        {
            var carrello = Session["Carrello"] as List<Pizze> ?? new List<Pizze>();
            return View(carrello);
        }

        public ActionResult AddToCart(int id)
        {
            using (var dbContext = new ModeldbContext())
            {
                var pizza = dbContext.Pizze.Find(id);
                if (pizza != null)
                {
                    var carrello = Session["Carrello"] as List<Pizze> ?? new List<Pizze>();
                    carrello.Add(pizza);
                    Session["Carrello"] = carrello;

                    TempData["Message"] = "Prodotto aggiunto al carrello con successo.";
                }
                else
                {
                    TempData["Message"] = "Errore: Prodotto non trovato.";
                }

                return RedirectToAction("Index", "Pizze");
            }
        }

        public ActionResult RemoveFromCart(int id)
        {
            using (var dbContext = new ModeldbContext())
            {
                var carrello = Session["Carrello"] as List<Pizze>;
                if (carrello != null)
                {
                    // Cerca la pizza nel carrello
                    var pizzaToRemove = carrello.FirstOrDefault(p => p.ID_Pizza == id);
                    if (pizzaToRemove != null)
                    {
                        // Rimuovi la pizza dal carrello
                        carrello.Remove(pizzaToRemove);
                        Session["Carrello"] = carrello;

                        TempData["Message"] = "Prodotto rimosso dal carrello con successo.";
                    }
                    else
                    {
                        TempData["Message"] = "Errore: Prodotto non trovato nel carrello.";
                    }
                }
                else
                {
                    TempData["Message"] = "Errore: Carrello non trovato.";
                }

                return RedirectToAction("Index", "Pizze");
            }
        }

    }
}