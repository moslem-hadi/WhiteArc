﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CMS.Models;
using ViewModels;

namespace CMS.Areas.Panel.Controllers
{
    [Authorize]
    public class IndexController : Controller
    {
         

        public ActionResult Index()
        {
            return View();
        }


         

    }
}