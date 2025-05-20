using Innovation.Development.DAL.Entities.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovation.Development.BLL.Services.Students
{
    public interface ISubjectService
    {
        IEnumerable<Subject> GetSubjects();
        //Subject GetSubject(int id);
        //void AddSubject(Subject subject);
        //void UpdateSubject(Subject subject);
        //void DeleteSubject(int id);
    }
}
