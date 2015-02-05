﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TicketingSystem.Data;
using TicketingSystem.Web.Models;
using TicketingSystem.Web.ViewModels;

using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace TicketingSystem.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        public HomeController(ITicketingSystemData data)
            :base(data)
        {
        }
        public ActionResult Index()
        {
            var tickets = Data.Tickets.All()
                .OrderByDescending(t => t.Comments.Count)
                .Take(6)
                .Project().To<TicketIndex>()
                .ToList();
            return View(tickets);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ticket = Data.Tickets.All()
                .Where(t=>t.Id == id.Value)
                .Project().To<TicketDetails>()
                .FirstOrDefault();
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}