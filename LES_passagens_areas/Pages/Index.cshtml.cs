﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LES_passagens_areas.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet(string cod)
        {
            if(!string.IsNullOrEmpty(cod))
            HttpContext.Session.SetObjectAsJson("login", null);
        }
    }
}
