using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsTicker.Entities;

namespace NewsTicker.Controllers
{
    [Route("api/[controller]")]
    public class NewsController : Controller
    {
        private NewsContext _db;

        public NewsController(NewsContext db)
        {
            _db = db;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetNews()
        {
            return Ok(await _db.Events.ToArrayAsync());
        }

        [HttpGet("group/{groupId}")]
        public async Task<IActionResult> GetNewsByGroup(int groupId)
        {
            var now = DateTime.Now;
            return Ok(await _db.Events.Where(e => e.ExpiresOn > now && e.Group == groupId).ToArrayAsync());
        }

        [HttpPost("seed")]
        public async Task<IActionResult> Test()
        {
            var success = new NewsEvent
            {
                Message = "Test Success Message",
                ExpiresOn = DateTime.Now.Add(TimeSpan.FromMinutes(20)),
                Group = 1,
                Severity = Severity.Success
            };
            var info = new NewsEvent
            {
                Message = "Test Info Message",
                ExpiresOn = DateTime.Now.Add(TimeSpan.FromMinutes(20)),
                Group = 1,
                Severity = Severity.Info
            };
            var warning = new NewsEvent
            {
                Message = "Test Warning Message",
                ExpiresOn = DateTime.Now.Add(TimeSpan.FromMinutes(20)),
                Group = 1,
                Severity = Severity.Warning
            };
            var critical = new NewsEvent
            {
                Message = "Test Critical Message",
                ExpiresOn = DateTime.Now.Add(TimeSpan.FromMinutes(20)),
                Group = 1,
                Severity = Severity.Critical
            };

            _db.Events.RemoveRange(await _db.Events.ToArrayAsync());
            await _db.Events.AddRangeAsync(success, info, warning, critical);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}