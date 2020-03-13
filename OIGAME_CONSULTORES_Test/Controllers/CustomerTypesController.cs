using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OIGAME_CONSULTORES_Test.Models;
using PagedList.Mvc;
using PagedList;

namespace OIGAME_CONSULTORES_Test.Controllers
{
    public class CustomerTypesController : Controller
    {
        private ModelOIGAMECONSULTORESTest db = new ModelOIGAMECONSULTORESTest();
        private static readonly log4net.ILog log4Net = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: CustomerTypes
        public async Task<ActionResult> Index(int? page, string search)
        {
            try
            {
                int pageZise = 10;
                int pageNumber = (page ?? 1);

                List<CustomerType> customerTypeList = new List<CustomerType>();


                if (search != null)
                {

                    customerTypeList = await db.CustomerType.Where(x => x.Description.StartsWith(search)).ToListAsync();
                }
                else
                {

                    customerTypeList = await db.CustomerType.ToListAsync();
                }

                return View(customerTypeList.ToPagedList(page ?? pageNumber, pageZise));
            }
            catch (Exception ex)
            {
                log4Net.Error(ex.Message, ex);
                throw;
            }
        }

        // GET: CustomerTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {

            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CustomerType customerType = await db.CustomerType.FindAsync(id);
                if (customerType == null)
                {
                    return HttpNotFound();
                }
                return View(customerType);
            }
            catch (Exception ex)
            {
                log4Net.Error(ex.Message, ex);
                throw;
            }
        }

        // GET: CustomerTypes/Create
        public ActionResult Create()
        {

            try
            {
                return View();
            }
            catch (Exception ex)
            {
                log4Net.Error(ex.Message, ex);
                throw;
            }
          
        }

        // POST: CustomerTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description")] CustomerType customerType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.CustomerType.Add(customerType);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                return View(customerType);
            }
            catch (Exception ex)
            {
                log4Net.Error(ex.Message, ex);
                throw;
            }
        }

        // GET: CustomerTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CustomerType customerType = await db.CustomerType.FindAsync(id);
                if (customerType == null)
                {
                    return HttpNotFound();
                }
                return View(customerType);
            }
            catch (Exception ex)
            {
                log4Net.Error(ex.Message, ex);
                throw;
            }
        }

        // POST: CustomerTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description")] CustomerType customerType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(customerType).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(customerType);
            }
            catch (Exception ex)
            {
                log4Net.Error(ex.Message, ex);
                throw;
            } 
        }

        // GET: CustomerTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CustomerType customerType = await db.CustomerType.FindAsync(id);
                if (customerType == null)
                {
                    return HttpNotFound();
                }
                return View(customerType);
            }
            catch (Exception ex)
            {
                log4Net.Error(ex.Message, ex);
                throw;
            }          
        }

        // POST: CustomerTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                CustomerType customerType = await db.CustomerType.FindAsync(id);
                db.CustomerType.Remove(customerType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                log4Net.Error(ex.Message, ex);
                throw;
            }       
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
