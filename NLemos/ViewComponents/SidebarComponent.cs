using Microsoft.AspNetCore.Mvc;
using NLemos.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLemos.ViewComponents
{
    [ViewComponent(Name = "Sidebar")]
    public class SidebarComponent : ViewComponent
    {
        private readonly ICreatorService _service;

        public SidebarComponent(ICreatorService service)
        {
            _service = service;
        }

        [ResponseCache(Duration = 3600 * 48)] //two days
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var creator = await _service.Get();
            return View(creator);
        }
    }
}
