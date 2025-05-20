using Innovation.Development.DAL.Entities.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovation.Development.DAL.contracts.Repositories
{
    public interface IStudentsRepository
    {
        IEnumerable<Student> GetAll(bool withTracking=false);
        Student? Get(int id);
        int Add(Student student);
        int Update(Student student);
        int Delete(int id);

    }
}
