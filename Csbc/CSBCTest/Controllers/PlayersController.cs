using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CSBC.Core.Data;
using CSBC.Core.Models;
using CSBC.Admin.Web.ViewModels;
using CSBC.Core.Repositories;

namespace CSBC.Admin.Web
{
    public class PlayersController : Controller
    {
        private CSBCDbContext db = new CSBCDbContext();

        // GET: Players
        public ActionResult Index()
        {
            var players = db.Players.Include(p => p.Division).Include(p => p.Person).Include(p => p.Season).Include(p => p.Team);
            return View(players.ToList());
        }

        // GET: Players/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: Players/Create
        public ActionResult Create()
        {
            ViewBag.DivisionID = new SelectList(db.Divisions, "DivisionID", "Div_Desc");
            ViewBag.PeopleID = new SelectList(db.People, "PeopleID", "FirstName");
            ViewBag.SeasonID = new SelectList(db.Seasons, "SeasonID", "Description");
            ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "TeamName");
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlayerID,CompanyID,SeasonID,DivisionID,TeamID,PeopleID,DraftID,DraftNotes,Rating,Coach,CoachID,Sponsor,SponsorID,AD,Scholarship,FamilyDisc,Rollover,OutOfTown,RefundBatchID,PaidDate,PaidAmount,BalanceOwed,PayType,NoteDesc,CheckMemo,CreatedDate,CreatedUser,PlaysDown,PlaysUp,ShoppingCartID")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Players.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DivisionID = new SelectList(db.Divisions, "DivisionID", "Div_Desc", player.DivisionID);
            ViewBag.PeopleID = new SelectList(db.People, "PeopleID", "FirstName", player.PeopleID);
            ViewBag.SeasonID = new SelectList(db.Seasons, "SeasonID", "Description", player.SeasonID);
            ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "TeamName", player.TeamID);
            return View(player);
        }

        // GET: Players/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            ViewBag.DivisionID = new SelectList(db.Divisions, "DivisionID", "Div_Desc", player.DivisionID);
            ViewBag.PeopleID = new SelectList(db.People, "PeopleID", "FirstName", player.PeopleID);
            ViewBag.SeasonID = new SelectList(db.Seasons, "SeasonID", "Description", player.SeasonID);
            ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "TeamName", player.TeamID);
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlayerID,CompanyID,SeasonID,DivisionID,TeamID,PeopleID,DraftID,DraftNotes,Rating,Coach,CoachID,Sponsor,SponsorID,AD,Scholarship,FamilyDisc,Rollover,OutOfTown,RefundBatchID,PaidDate,PaidAmount,BalanceOwed,PayType,NoteDesc,CheckMemo,CreatedDate,CreatedUser,PlaysDown,PlaysUp,ShoppingCartID")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DivisionID = new SelectList(db.Divisions, "DivisionID", "Div_Desc", player.DivisionID);
            ViewBag.PeopleID = new SelectList(db.People, "PeopleID", "FirstName", player.PeopleID);
            ViewBag.SeasonID = new SelectList(db.Seasons, "SeasonID", "Description", player.SeasonID);
            ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "TeamName", player.TeamID);
            return View(player);
        }

        // GET: Players/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Find(id);
            db.Players.Remove(player);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PlayerHistory(int peopleId)
        {
            using (var db = new CSBCDbContext())
            {
                var rep = new PlayerRepository(db);
                var players = rep.GetPlayerHistory(peopleId);
                var viewPlayers = new List<PlayerHistory>();
                
                return View(viewPlayers);
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
