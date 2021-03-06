﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureTableStorageCacheSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDistributedCache cacheMechanism;

        public HomeController(IDistributedCache cacheMechanism)
        {
            this.cacheMechanism = cacheMechanism;
        }

        public async Task<IActionResult> Index()
        {
            await cacheMechanism.SetAsync("awesomeRecord2", Encoding.UTF32.GetBytes("records are awesome"), new DistributedCacheEntryOptions() { SlidingExpiration = new TimeSpan(0, 5, 0) });
            var data = await cacheMechanism.GetAsync("awesomeRecord2");
            var result = string.Empty;
            if (data != null)
            {
                result = Encoding.UTF32.GetString(data);
            }
            else
            {
            }
            return View(result);
        }

        public async Task<IActionResult> AddCache()
        {
            await cacheMechanism.SetAsync("awesomeRecord", Encoding.UTF32.GetBytes("Im Awesome"));
            ViewData["Message"] = "Your application description page.";

            return RedirectToAction("Index");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}