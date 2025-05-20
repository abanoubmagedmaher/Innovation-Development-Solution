using Innovation.Development.DAL.contracts.Repositories;
using Innovation.Development.DAL.Entities.Students;
using Innovation.Development.DAL.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovation.Development.DAL.Persistence.Repositories
{
    public class SubjectRepository : ISubjectsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SubjectRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Subject> GetAll(bool withTracking = false)
        {
            if (!withTracking)
            {
                return _dbContext.Subjects.AsNoTracking().ToList();
            }
            return _dbContext.Subjects.ToList();
        }
    }
}
