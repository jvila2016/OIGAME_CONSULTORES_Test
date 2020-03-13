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
using OIGAME_CONSULTORES_Test.Utilities;

namespace OIGAME_CONSULTORES_Test.Controllers
{
    public class CustomersController : Controller
    {
        private ModelOIGAMECONSULTORESTest db = new ModelOIGAMECONSULTORESTest();
        private static readonly log4net.ILog log4Net = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: Customers
        public async Task<ActionResult> Index(int? page, string search)
        {


            try
            {
                int pageZise = 10;
                int pageNumber = (page ?? 1);

                List<Customer> customerList = new List<Customer>();

                if (search != null)
                {

                    customerList = await db.Customer.Where(x => x.FirstName.StartsWith(search) && x.Active != false).ToListAsync();
                }
                else
                {

                    customerList = await db.Customer.Where(x => x.Active != false).ToListAsync();
                }

                return View(customerList.ToPagedList(page ?? pageNumber, pageZise));
            }
            catch (Exception ex)
            {
                log4Net.Error(ex.Message, ex);
                throw;
            }

            
        }

        public async Task<ActionResult> IndexFromSP(int? page, string search)
        {

            try
            {

                int pageZise = 10;
                int pageNumber = (page ?? 1);

                List<Customer> customerList = DataHandler.GetCustomerfromSPT();

                if (search != null)
                {

                    customerList = await db.Customer.Where(x => x.FirstName.StartsWith(search) && x.Active != false).ToListAsync();
                }
                else
                {

                    customerList = await db.Customer.Where(x => x.Active != false).ToListAsync();
                }

                return View(customerList.ToPagedList(page ?? pageNumber, pageZise));
            }
            catch (Exception ex)
            {
                log4Net.Error(ex.Message, ex);
                throw;
            }

        }

        public ActionResult IndexFromSPAllCustomerType1(int? page, string search)
        {
            try
            {

                int pageZise = 10;
                int pageNumber = (page ?? 1);

                List<Customer1> customerList = DataHandler.AllCustomerType1();

                return View(customerList.ToPagedList(page ?? pageNumber, pageZise));
            }
            catch (Exception ex)
            {
                log4Net.Error(ex.Message, ex);
                throw;
            }

        }

        [HttpGet]
        public  ActionResult DeleteAllCustomerEnding7()
        {

            try
            {

                DataHandler.DeleteAllCustomerEnding7();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                log4Net.Error(ex.Message, ex);
                throw;
            }

        }

        // GET: Customers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Customer customer = await db.Customer.FindAsync(id);
                if (customer == null)
                {
                    return HttpNotFound();
                }
                return View(customer);
            }
            catch (Exception ex)
            {
                log4Net.Error(ex.Message, ex);
                throw;
            }        
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            try
            {

                ViewBag.CustomerTypeId = new SelectList(db.CustomerType, "Id", "Description");
                return View();
            }
            catch (Exception ex)
            {
                log4Net.Error(ex.Message, ex);
                throw;
            }
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CustomerTypeId,Passport,FirstName,LastName,Active")] Customer customer)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    db.Customer.Add(customer);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                ViewBag.CustomerTypeId = new SelectList(db.CustomerType, "Id", "Description", customer.CustomerTypeId);
                return View(customer);
            }
            catch (Exception ex)
            {
                log4Net.Error(ex.Message, ex);
                throw;
            }    
        }


        public bool StoredProcedureInsertCustomer(int? CustomerTypeId, string Passport, string FirstName, string LastName, bool Active)
        {

            try
            {

                return DataHandler.Insert_Customer(CustomerTypeId, Passport, FirstName, LastName, Active);
            }
            catch (Exception ex)
            {
                log4Net.Error(ex.Message, ex);
                throw;
            }
          
        }


        [HttpGet]
        public ActionResult CreateCustomewrFromSP()
        {

            try
            {

                ViewBag.CustomerTypeId = new SelectList(db.CustomerType, "Id", "Description");
                return View();
            }
            catch (Exception ex)
            {
                log4Net.Error(ex.Message, ex);
                throw;
            }

        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCustomewrFromSP([Bind(Include = "CustomerTypeId,Passport,FirstName,LastName,Active")] Customer customer)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    if (StoredProcedureInsertCustomer(customer.CustomerTypeId, customer.Passport, customer.FirstName, customer.LastName, customer.Active))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {

                        ViewBag.CustomerTypeId = new SelectList(db.CustomerType, "Id", "Description", customer.CustomerTypeId);
                        return View(customer);
                    }

                }

                ViewBag.CustomerTypeId = new SelectList(db.CustomerType, "Id", "Description", customer.CustomerTypeId);
                return View(customer);
            }
            catch (Exception ex)
            {
                log4Net.Error(ex.Message, ex);
                throw;
            }
 
        }


        // GET: Customers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {

            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Customer customer = await db.Customer.FindAsync(id);
                if (customer == null)
                {
                    return HttpNotFound();
                }
                ViewBag.CustomerTypeId = new SelectList(db.CustomerType, "Id", "Description", customer.CustomerTypeId);
                return View(customer);
            }
            catch (Exception ex)
            {
                log4Net.Error(ex.Message, ex);
                throw;
            }
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CustomerTypeId,Passport,FirstName,LastName,Active")] Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(customer).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                ViewBag.CustomerTypeId = new SelectList(db.CustomerType, "Id", "Description", customer.CustomerTypeId);
                return View(customer);
            }
            catch (Exception ex)
            {
                log4Net.Error(ex.Message, ex);
                throw;
            }      
        }

        // GET: Customers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Customer customer = await db.Customer.FindAsync(id);
                if (customer == null)
                {
                    return HttpNotFound();
                }
                return View(customer);
            }
            catch (Exception ex)
            {
                log4Net.Error(ex.Message, ex);
                throw;
            }
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {

                Customer customer = await db.Customer.FindAsync(id);
                customer.Active = false;
                //db.Customer.Remove(customer);
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

        [HttpGet]
        public string PastportExist(string Passport)
        {


            try
            {

                if (DataHandler.PastportExist(Passport))
                {

                    return "false";
                }
                else
                    return "true";
            }
            catch (Exception ex)
            {
                log4Net.Error(ex.Message, ex);
                throw;
            }

           
        }
    }
}
