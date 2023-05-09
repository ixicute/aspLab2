using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkolprojektLab2.Data;
using SkolprojektLab2.Models;
using SkolprojektLab2.ViewModels;

namespace SkolprojektLab2.Controllers
{
    public class RelationshipController : Controller
    {
        private readonly ApplicationDbContext context;

        public RelationshipController(ApplicationDbContext _context)
        {
            context = _context;
        }
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = context.Relationships.Include(r => r.Classes).Include(r => r.Courses).Include(r => r.Students).Include(r => r.Teachers);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Edit(int? id = 1)
        {
            var relationshipTable = await context.Relationships.FindAsync(id);

            var viewModel = new RelationshipViewModel
            {
                RTId = relationshipTable.Id,
                ClassId = relationshipTable.FK_ClassId,
                CourseId = relationshipTable.FK_CourseId,
                StudentId = relationshipTable.FK_StudentId,
                TeacherId = relationshipTable.FK_TeacherId
            };

            ViewData["FK_ClassId"] = new SelectList(
                context.Classes.Select(c => new { Value = c.ClassId, Text = c.ClassName }),
                "Value",
                "Text",
                relationshipTable.FK_ClassId
            );

            ViewData["FK_CourseId"] = new SelectList(
                context.Courses.Select(c => new { Value = c.CourseId, Text = c.CourseName }),
                "Value",
                "Text",
                relationshipTable.FK_CourseId
            );

            ViewData["FK_StudentId"] = new SelectList(
                context.Students.Select(s => new { Value = s.StudentId, Text = s.FirstName }),
                "Value",
                "Text",
                relationshipTable.FK_StudentId
            );

            ViewData["FK_TeacherId"] = new SelectList(
                context.Teachers.Select(t => new { Value = t.TeacherId, Text = t.FirstName }),
                "Value",
                "Text",
                relationshipTable.FK_TeacherId
            );

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RelationshipViewModel relationshipViewModel)
        {
            if (ModelState.IsValid)
            {
                var relationTable = await context.Relationships.FindAsync(relationshipViewModel.RTId);

                relationTable.FK_StudentId = relationshipViewModel.StudentId;
                relationTable.FK_CourseId = relationshipViewModel.CourseId;
                relationTable.FK_TeacherId = relationshipViewModel.TeacherId;
                relationTable.FK_ClassId = relationshipViewModel.ClassId;

                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<IActionResult> GetTeacherBySubject(string searchString = "")
        {
            var teacherResult = await (from r in context.Relationships
                                       join t in context.Teachers on r.FK_TeacherId equals t.TeacherId
                                       join c in context.Courses on r.FK_CourseId equals c.CourseId
                                       where c.CourseName.Contains(searchString)
                                       group t by new { c.CourseId, c.CourseName, t.TeacherId, t.FirstName, t.LastName } into g
                                       select new
                                       {
                                           CourseName = g.Key.CourseName,
                                           CourseId = g.Key.CourseId,
                                           TeacherName = g.Key.FirstName + " " + g.Key.LastName,
                                           TeacherId = g.Key.TeacherId
                                       }).ToListAsync();

            List<TeacherCourseSearchViewModel> searchResult = new List<TeacherCourseSearchViewModel>();

            foreach (var item in teacherResult)
            {
                var dataSelected = new TeacherCourseSearchViewModel
                {
                    TeacherName = item.TeacherName,
                    CourseName = item.CourseName,
                    FK_RelationshipTable = context.Relationships
                    .FirstOrDefault(r => r.FK_TeacherId == item.TeacherId && r.FK_CourseId == item.CourseId)?.Id ?? 0
                };
                searchResult.Add(dataSelected);
            }

            searchResult = searchResult.OrderBy(x => x.FK_RelationshipTable).ToList();

            return View(searchResult);
        }

        public async Task<IActionResult> GetStudentBySubject(string searchString = "")
        {
            var studentResult = await (from r in context.Relationships
                                       join s in context.Students on r.FK_StudentId equals s.StudentId
                                       join t in context.Teachers on r.FK_TeacherId equals t.TeacherId
                                       join c in context.Courses on r.FK_CourseId equals c.CourseId
                                       where c.CourseName.Contains(searchString)
                                       group s by new {c.CourseId, c.CourseName, s.StudentId, s.FirstName, s.LastName, TeacherFirstName = t.FirstName, TeacherLastName = t.LastName, t.TeacherId } into g
                                       select new
                                       {
                                           CourseName = g.Key.CourseName,
                                           CourseId = g.Key.CourseId,
                                           StudentName = g.Key.FirstName + " " + g.Key.LastName,
                                           StudentId = g.Key.StudentId,
                                           TeacherName = g.Key.TeacherFirstName + " " + g.Key.TeacherLastName,
                                           TeacherId = g.Key.TeacherId
                                       }).ToListAsync();

            List<StudentTeacherSearchViewModel> searchResult = new List<StudentTeacherSearchViewModel>();

            foreach (var item in studentResult)
            {
                var dataSelected = new StudentTeacherSearchViewModel
                {
                    StudentName = item.StudentName,
                    TeacherName = item.TeacherName,
                    CourseName = item.CourseName,
                    CourseId = item.CourseId,
                    FK_RelationshipTable = context.Relationships
                        .FirstOrDefault(r => r.FK_StudentId == item.StudentId && r.FK_CourseId == item.CourseId && r.FK_TeacherId == item.TeacherId)?.Id ?? 0
                };
                searchResult.Add(dataSelected);
            }

            searchResult = searchResult.OrderBy(x => x.FK_RelationshipTable).ToList();

            return View(searchResult);
        }

        public async Task<IActionResult> EditSubjectName(int? id = 1)
        {
            if (id == null || context.Courses == null)
            {
                return NotFound();
            }

            var course = await context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSubjectName(int id, [Bind("CourseId,CourseName")] Course course)
        {
            if (id != course.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                context.Update(course);
                await context.SaveChangesAsync();

                return RedirectToAction("Index", "Relationship");
            }
            return View(course);
        }
    }
}