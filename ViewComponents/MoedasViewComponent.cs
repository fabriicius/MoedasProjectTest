using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoedasCrypt.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoedasCrypt.ViewComponents
{
    public class MoedasViewComponent : ViewComponent
    {
        private readonly MoedasContext _moedasContext; 

        public MoedasViewComponent(MoedasContext moedasContext)
        {
            _moedasContext = moedasContext ;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _moedasContext.Moedas.ToListAsync());
        }

    }
}
