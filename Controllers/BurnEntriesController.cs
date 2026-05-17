using BurnBook.Data;
using BurnBook.Models;
using BurnBook.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BurnBook.Controllers
{
    public class BurnEntriesController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public BurnEntriesController(
            ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>
        Add(AddBurnEntryViewModel viewModel)
        {
            var entry = new BurnEntry()
            {
                Nickname = viewModel.Nickname,
                Message = viewModel.Message,
                Category = viewModel.Category,
                DateCreated = DateTime.Now
            };

            await dbContext.BurnEntries
                .AddAsync(entry);

            await dbContext.SaveChangesAsync();

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var entries = await dbContext
                .BurnEntries
                .ToListAsync();

            return View(entries);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var entry = await dbContext.BurnEntries.FindAsync(id);

            if (entry == null)
            {
                return NotFound();
            }

            var viewModel = new AddBurnEntryViewModel
            {
                Nickname = entry.Nickname,
                Message = entry.Message,
                Category = entry.Category
            };

            ViewBag.Id = id;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, AddBurnEntryViewModel viewModel)
        {
            var entry = await dbContext.BurnEntries.FindAsync(id);

            if (entry == null)
            {
                return NotFound();
            }

            entry.Nickname = viewModel.Nickname;
            entry.Message = viewModel.Message;
            entry.Category = viewModel.Category;

            await dbContext.SaveChangesAsync();

            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var entry = await dbContext.BurnEntries.FindAsync(id);

            if (entry == null)
            {
                return NotFound();
            }

            dbContext.BurnEntries.Remove(entry);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("List");
        }
    }
}