using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AspNetMVC.Models;
using AspNetMVC.Models.Entity;

namespace AspNetMVC.Controllers
{
    public class MemberCRUDController : Controller
    {
        private UCleanerDBContext db = new UCleanerDBContext();

        // GET: MemberCRUD
        public ActionResult Index()
        {
            return View(db.MemberMds.ToList());
        }

        // GET: MemberCRUD/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberMd memberMd = db.MemberMds.Find(id);
            if (memberMd == null)
            {
                return HttpNotFound();
            }
            return View(memberMd);
        }

        // GET: MemberCRUD/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemberCRUD/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccountId,Name,CreditNumber,ExpiryDate,SafeNum,CreateTime,CreateUser,EditTime,EditUser")] MemberMd memberMd)
        {
            if (ModelState.IsValid)
            {
                memberMd.AccountId = Guid.NewGuid();
                db.MemberMds.Add(memberMd);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(memberMd);
        }

        // GET: MemberCRUD/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberMd memberMd = db.MemberMds.Find(id);
            if (memberMd == null)
            {
                return HttpNotFound();
            }
            return View(memberMd);
        }

        // POST: MemberCRUD/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountId,Name,CreditNumber,ExpiryDate,SafeNum,CreateTime,CreateUser,EditTime,EditUser")] MemberMd memberMd)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberMd).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(memberMd);
        }

        // GET: MemberCRUD/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberMd memberMd = db.MemberMds.Find(id);
            if (memberMd == null)
            {
                return HttpNotFound();
            }
            return View(memberMd);
        }

        // POST: MemberCRUD/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            MemberMd memberMd = db.MemberMds.Find(id);
            db.MemberMds.Remove(memberMd);
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
