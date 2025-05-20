using Innovation.Development.DAL.contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovation.Development.DAL.contracts
{
    public interface IUnitOfWork
    {
        public IStudentsRepository StudentsRepository { get; set; }
        public ISubjectsRepository SubjectsRepository { get; set; }
        void Dispose();
        int Complete();
    }
}
