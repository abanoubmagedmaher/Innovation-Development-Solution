using Innovation.Development.DAL.contracts;
using Innovation.Development.DAL.contracts.Repositories;
using Innovation.Development.DAL.Persistence.Data;
using Innovation.Development.DAL.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovation.Development.DAL.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IStudentsRepository StudentsRepository { get; set; }
        public ISubjectsRepository SubjectsRepository { get ; set ; }
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            StudentsRepository = new StudentRepository(dbContext);
            SubjectsRepository = new SubjectRepository(dbContext);
            _dbContext = dbContext;
        }
        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
