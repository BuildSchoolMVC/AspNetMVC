﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMVC.Controllers
{
    public class ProductPageController : Controller
    {
        // GET: ProductPage
        public ActionResult Index()
        {
            return View();
        }
    }
}