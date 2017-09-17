using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using InterestRateCalculation.Models;
using InterestRateCalculation.Shared;

namespace InterestRateCalculation.Controllers
{
    public class AgreementsController : Controller
    {
        private InterestRateContext db = new InterestRateContext();

        // GET: Agreements
        public ActionResult Index()
        {
            var agreements = db.Agreements.Include(a => a.Customer);
            return View(agreements.ToList());
        }

        // GET: Agreements/Details/5
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Agreement agreement = db.Agreements.Find(id);

            if (agreement == null)
            {
                return HttpNotFound();
            }

            agreement.InterestRate = InterBankRates.GetInterestRate(agreement.BaseRate.BaseRateCode, agreement.Margin);

            ViewBag.NewBaseRateId = GetBaseRatesDropdown();

            return View(agreement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(int? id, int? newBaseRateId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Agreement agreement = db.Agreements.Find(id);
            if (agreement == null)
            {
                return HttpNotFound();
            }

            if (newBaseRateId == null)
            {
                return View(agreement);
            }

            BaseRate baseRate = db.BaseRates.Find(newBaseRateId);

            agreement.InterestRate = InterBankRates.GetInterestRate(agreement.BaseRate.BaseRateCode, agreement.Margin);
            agreement.NewInterestRate = InterBankRates.GetInterestRate(baseRate.BaseRateCode, agreement.Margin);
            agreement.InterestRatesDiff = agreement.InterestRate - agreement.NewInterestRate;
            agreement.NewBaseRateId = (int)newBaseRateId;

            ViewBag.NewBaseRateId = GetBaseRatesDropdown();

            return View(agreement);
        }


        // GET: Agreements/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = GetCustomersDropDown();
            ViewBag.BaseRateId = GetBaseRatesDropdown();
            return View();
        }

        // POST: Agreements/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Amount,Margin,BaseRateCode,Duration,CustomerId,BaseRateId")] Agreement agreement)
        {
            if (ModelState.IsValid)
            {
                db.Agreements.Add(agreement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = GetCustomersDropDown();
            ViewBag.BaseRateId = GetBaseRatesDropdown();
            return View(agreement);
        }

        // GET: Agreements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Agreement agreement = db.Agreements.Find(id);
            if (agreement == null)
            {
                return HttpNotFound();
            }

            agreement.InterestRate = InterBankRates.GetInterestRate(agreement.BaseRate.BaseRateCode, agreement.Margin);
            ViewBag.CustomerId = GetCustomersDropDown(agreement.CustomerId);
            ViewBag.BaseRateId = GetBaseRatesDropdown(agreement.BaseRateId);

            return View(agreement);
        }

        // POST: Agreements/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Amount,Margin,BaseRateCode,Duration,CustomerId,BaseRateId")] Agreement model)
        {
            Agreement agreement = model;
            if (ModelState.IsValid)
            {
                db.Entry(agreement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = GetCustomersDropDown();
            ViewBag.BaseRateId = GetBaseRatesDropdown();

            return View(agreement);
        }

        // GET: Agreements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agreement agreement = db.Agreements.Find(id);
            if (agreement == null)
            {
                return HttpNotFound();
            }
            return View(agreement);
        }

        // POST: Agreements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agreement agreement = db.Agreements.Find(id);
            db.Agreements.Remove(agreement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private SelectList GetCustomersDropDown()
        {
            return GetCustomersDropDown(null);
        }

        private SelectList GetCustomersDropDown(int? selected)
        {
            return new SelectList(db.Customers, "Id", "Name", selected);
        }

        private SelectList GetBaseRatesDropdown()
        {
            return GetBaseRatesDropdown(null);
        }

        private SelectList GetBaseRatesDropdown(int? selected)
        {
            return new SelectList(db.BaseRates, "Id", "BaseRateCode", selected ?? null);
        }

        [HttpPost]
        public ActionResult GetNewInterestRate(int? id, int? val)
        {
            if (id != null && val != null)
            {
                Agreement agreement = db.Agreements.Find(id);
                BaseRate baseRate = db.BaseRates.Find(val);

                var InterestRate = InterBankRates.GetInterestRate(agreement.BaseRate.BaseRateCode, agreement.Margin);
                var NewInterestRate = InterBankRates.GetInterestRate(baseRate.BaseRateCode, agreement.Margin);
                return Json(new {
                    Success = "true",
                    Data = new {
                        CurrentInterestRate = InterestRate,
                        NewInterestRate = NewInterestRate,
                        InterestRatesDiff = InterestRate - NewInterestRate
                    }
                });
            }
            return Json(new { Success = "false" });
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
