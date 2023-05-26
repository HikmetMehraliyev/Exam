using ExamProject5.DAL;
using ExamProject5.Models;
using ExamProject5.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject5.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<TeamMember> teamMembers = await _context.TeamMembers.OrderByDescending(t => t.Id).Take(4).ToListAsync();
            return View(teamMembers);
        }
    }
}
