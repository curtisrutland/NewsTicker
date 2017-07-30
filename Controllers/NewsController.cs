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

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            var test = new NewsEvent
            {
                Message = "test",
                ExpiresOn = DateTime.Now.Add(TimeSpan.FromDays(1)),
                Group = 1,
                Severity = Severity.Success
            };
            await _db.Events.AddAsync(test);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}