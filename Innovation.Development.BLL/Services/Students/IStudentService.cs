using Innovation.Development.BLL.Models.Students;
using Innovation.Development.DAL.Entities.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovation.Development.BLL.Services.Students
{
    public interface IStudentService
    {
        IEnumerable<StudentDto> GetStudents();
        StudentDto? GetStudent(int id);
        int CreateStudent(CreateStudentDto student);
        int UpdateStudent(StudentDto student);
        bool DeleteStudent(int id);
    }
}
