using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Data;
using E_Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using E_Commerce.Services;
using Microsoft.AspNetCore.Http;

namespace E_Commerce.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ECommerceDbContext _context;
        private readonly IFileUploadService fileUploadService;

        public ProductsController(ECommerceDbContext context, IFileUploadService fileUploadService )
        {
            _context = context;
            this.fileUploadService = fileUploadService;
        }

        //[Authorize(Roles = "Administrator")]

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var eCommerceDbContext = _context.Product.Include(p => p.ProductCategory);
            return View(await eCommerceDbContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.ProductCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            if (!User.IsInRole("Administrator"))
                return NotFound();

            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,InventoryAmount,Summary,Condition,ProductCategoryId")] Product product)
        {
            if (!User.IsInRole("Administrator"))
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "Id", "Name", product.ProductCategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> Edit(int? id)
        {

            if (!User.IsInRole("Editor"))
                return NotFound();


            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "Id", "Name", product.ProductCategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,InventoryAmount,Summary,Condition,ProductCategoryId")] Product product)
        {

            if (!User.IsInRole("Editor"))
                return NotFound();

            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "Id", "Name", product.ProductCategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.IsInRole("Administrator"))
                return NotFound();

            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.ProductCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!User.IsInRole("Administrator"))
                return NotFound();

            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<IActionResult> UploadProductImage(IFormFile productImage)
        {
            string url = await fileUploadService.Upload(productImage);
            await fileUploadService.SetProductImage(Product.ProductImage, url);
            return RedirectToAction(nameof(Index));
        }
    }
}
