using CateringServiceManagerAspNetCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    // Constructor to initialize the database context
    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Admin/Register
    // Displays the registration form for a new admin
    public IActionResult Register()
    {
        return View();
    }

    // POST: Admin/Register
    // Handles the registration form submission
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register([Bind("Email,Password")] Admin admin)
    {
        if (ModelState.IsValid)
        {
            _context.Add(admin); // Adds the new admin to the database
            _context.SaveChanges(); // Saves the changes to the database
            return RedirectToAction(nameof(Login)); // Redirects to the login page
        }
        return View(admin); // Returns the form view with validation errors if any
    }

    // GET: Admin/Login
    // Displays the login form for the admin
    public IActionResult Login()
    {
        return View();
    }

    // POST: Admin/Login
    // Handles the login form submission
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login([Bind("Email,Password")] Admin admin)
    {
        var existingAdmin = _context.tbl_admin
            .FirstOrDefault(a => a.Email == admin.Email && a.Password == admin.Password); // Checks for matching admin credentials

        if (existingAdmin != null)
        {
            // Sets the admin email in session to maintain the login state
            HttpContext.Session.SetString("AdminEmail", admin.Email);
            return RedirectToAction("Dashboard"); // Redirects to the dashboard if login is successful
        }

        // Adds an error message if login fails
        ModelState.AddModelError("", "Invalid login attempt.");
        return View(admin); // Returns the login form with the error message
    }

    // GET: Admin/Dashboard
    // Displays the admin dashboard if the admin is logged in
    public IActionResult Dashboard()
    {
        var row = HttpContext.Session.GetString("AdminEmail");
        if (row != null)
        {
            return View(); // Displays the dashboard if the admin is authenticated
        }
        return RedirectToAction("Login"); // Redirects to the login page if the admin is not authenticated
    }

    // GET: Admin/Logout
    // Logs out the admin by removing the session data
    public IActionResult Logout()
    {
        HttpContext.Session.Remove("AdminEmail"); // Removes the admin email from session
        return RedirectToAction(nameof(Login)); // Redirects to the login page after logout
    }
}
