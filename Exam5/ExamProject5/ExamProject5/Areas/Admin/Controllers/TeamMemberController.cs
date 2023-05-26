using ExamProject5.DAL;
using ExamProject5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using ExamProject5.Areas.Admin.ViewModels;
using ExamProject5.Utilities.Constants;
using ExamProject5.Utilities.Messages;
using System.IO;

namespace ExamProject5.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamMemberController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;
        private readonly string _rootPath;
        public TeamMemberController(AppDbContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
            _rootPath = Path.Combine(_webHostEnviroment.WebRootPath, "assets", "img", "team");
        }

        public async Task<IActionResult> Index()
        {
            List<TeamMember> teamMember = await _context.TeamMembers.OrderByDescending(t => t.Id).ToListAsync();
            return View(teamMember);
        }
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMemberVM createmember)
        {
            if (!ModelState.IsValid) return View(createmember);
            if (!createmember.Photo.CheckContentType("image/"))
            {
                ModelState.AddModelError("Photo", ErrorMessages.FileMustBeImageTaype);
                return View(createmember);
            }
            if (!createmember.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", ErrorMessages.FileMustBeImageSize);
                return View(createmember);

            }
            string filename = await createmember.Photo.SaveAsync(_rootPath);
            TeamMember teamMember = new TeamMember()
            {
                Name = createmember.Name,
                Surname = createmember.Surname,
                Description = createmember.Description,
                Profession = createmember.Profession,
                ImagePath = filename,
            };

            await _context.TeamMembers.AddAsync(teamMember);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Update(int id)
        {
            TeamMember teamMember = await _context.TeamMembers.FindAsync(id);
            if (teamMember == null) { return NotFound(); }
            UpdateMemberVM updateMember = new UpdateMemberVM()
            {
                Name = teamMember.Name,
                Surname = teamMember.Surname,
                Description = teamMember.Description,
                Profession = teamMember.Profession,
                Id = id
            };
            return View(updateMember);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateMemberVM updateMember)
        {
            if (!ModelState.IsValid) return View(updateMember);
            if (updateMember.Photo.CheckContentType("image/"))
            {
                ModelState.AddModelError("Photo", ErrorMessages.FileMustBeImageTaype);
                return View(updateMember);
            }
            if (updateMember.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", ErrorMessages.FileMustBeImageSize);
                return View(updateMember);
            }
            string filename = (await _context.TeamMembers.FindAsync(updateMember.Id))?.ImagePath;
            string filepath = Path.Combine(_rootPath, filename);
            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }
            await updateMember.Photo.SaveAsync(_rootPath);


            string newfilename = await updateMember.Photo.SaveAsync(_rootPath);
            TeamMember teamMember = new TeamMember()
            {
                Name = updateMember.Name,
                Surname = updateMember.Surname,
                Description = updateMember.Description,
                Profession = updateMember.Profession,
                ImagePath = newfilename,
                Id = updateMember.Id
            };
            return View(updateMember);

        }


        public async Task<IActionResult> Delete(int id)
        {
            TeamMember teamMember = await _context.TeamMembers.FindAsync(id);
            if (teamMember == null) { return NotFound(); }
            string filepath = Path.Combine(_rootPath, teamMember.ImagePath);
            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }
            _context.TeamMembers.Remove(teamMember);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
