using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsTicker.Entities;
using NewsTicker.Models;

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

        [HttpGet("group/{groupId}/{pageSize?}")]
        public async Task<IActionResult> GetNewsByGroup(int groupId, int pageSize = 10)
        {
            var now = DateTime.Now;
            var events = await _db.Events
                .Where(e => e.Group == groupId)
                .OrderByDescending(e => e.CreatedOn)
                .Take(pageSize)
                .ToArrayAsync();
            return Ok(events);
        }

        [HttpPost]
        public async Task<IActionResult> Publish([FromBody] CreateNewsModel news)
        {
            var ev = news.ToNewsEvent();
            await _db.Events.AddAsync(ev);
            await _db.SaveChangesAsync();
            return Ok(ev.Id);
        }

        [HttpDelete("reset")]
        public async Task<IActionResult> Reset()
        {
            _db.RemoveRange(await _db.Events.ToArrayAsync());
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("seed")]
        public async Task<IActionResult> Test()
        {
            var success = new NewsEvent
            {
                Message = "Lorem ipsum dolor sit amet",
                Group = 1,
                Severity = Severity.Success
            };
            var info = new NewsEvent
            {
                Message = "Suspendisse eu aliquet ligula",
                Group = 1,
                Severity = Severity.Info
            };
            var warning = new NewsEvent
            {
                Message = "Cras ut interdum ligula",
                Group = 1,
                Severity = Severity.Warning
            };
            var critical = new NewsEvent
            {
                Message = "Integer a nisl mattis",
                Group = 1,
                Severity = Severity.Critical
            };

            var success2 = new NewsEvent
            {
                Message = "Phasellus rutrum mauris in libero",
                Group = 2,
                Severity = Severity.Success
            };
            var info2 = new NewsEvent
            {
                Message = "Vivamus pretium ligula a egestas faucibus",
                Group = 2,
                Severity = Severity.Info
            };
            var warning2 = new NewsEvent
            {
                Message = "Suspendisse potenti",
                Group = 2,
                Severity = Severity.Warning
            };
            var critical2 = new NewsEvent
            {
                Message = "Phasellus eget elit finibus",
                Group = 2,
                Severity = Severity.Critical
            };

            _db.Events.RemoveRange(await _db.Events.ToArrayAsync());
            await _db.Events.AddRangeAsync(info, success, warning, critical, warning2, success2, info2, critical2);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}