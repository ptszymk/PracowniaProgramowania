#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracowniaProgramowania.Data;
using PracowniaProgramowania.Models;
using System.Security.Cryptography;
using System.Text;
using X.PagedList;

namespace PracowniaProgramowania.Controllers
{
    public static class Hashmasz
    {
        public static string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
    }
    public class UsersController : Controller
    {
        private readonly PracowniaProgramowaniaContext _context;

        public UsersController(PracowniaProgramowaniaContext context)
        {
            _context = context;
        }


        // GET: Users
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var notDeleted = _context.Users.Where(x => x.IsDeleted == false).ToPagedList(pageNumber, pageSize);
            return View(notDeleted);
        }

        public async Task<IActionResult> Index2(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var deleted = _context.Users.Where(x => x.IsDeleted == true).ToPagedList(pageNumber, pageSize);
            return View(deleted);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,DateOfBirth,Login,Password,RoleId,IsDeleted")] Users users)
        {
            ModelState.Remove("Roles");
            if (ModelState.IsValid)
            {
                users.Password = Hashmasz.ComputeHash(users.Password, new SHA256CryptoServiceProvider()).Replace("-", "");
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,DateOfBirth,Login,Password,RoleId,IsDeleted")] Users users)
        {
            if (id != users.Id)
            {
                return NotFound();
            }
            ModelState.Remove("Roles");

            if (ModelState.IsValid)
            {
                try
                {
                    if (users.Password.Length < 30) { users.Password = Hashmasz.ComputeHash(users.Password, new SHA256CryptoServiceProvider()).Replace("-", ""); }
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.Id))
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
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.FindAsync(id);
            _context.Users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
