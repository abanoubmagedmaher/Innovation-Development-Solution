using Innovation.Development.DAL.Entities.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovation.Development.BLL.Services.Subjects
{
    public interface ISubjectService
    {
        IEnumerable<Subject> GetAllSubjects();
    }
}