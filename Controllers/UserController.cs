using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using CustomERPSolution_Invoices_Currency.Data;
using CustomERPSolution_Invoices_Currency.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDbContext _context;

    public UserController(UserManager<User> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    // GET: /User/Index
    public IActionResult Index()
    {
        var users = _userManager.Users.ToList();
        return View(users);
    }

    // GET: /User/Details/{id}
    public async Task<IActionResult> Details(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    // GET: /User/Edit/{id}
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    // POST: /User/Edit/{id}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, User user)
    {
        if (id != user.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
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
        return View(user);
    }

    private bool UserExists(string id)
    {
        return _userManager.Users.Any(e => e.Id == id);
    }
}
