using Innovation.Development.BLL.Services.Students;
using Microsoft.AspNetCore.Mvc;

namespace Innovation_Development.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
