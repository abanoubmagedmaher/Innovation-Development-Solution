using Innovation.Development.DAL.Entities.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovation.Development.BLL.Models.Students
{
    public record CreateStudentDto(string Name, DateOnly DateOfBirth,
        string Address, ICollection<Subject> Subjects);
    
}
