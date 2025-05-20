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
    public class StudentRepository : IStudentsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Student> GetAll(bool withTracking = false)
        {
            if (!withTracking)
            {
                return _dbContext.Students.AsNoTracking().ToList();
            }
            return _dbContext.Students.ToList();
            
        }
        public Student? Get(int id)
        {
            var Students = _dbContext.Students.Find(id);
            return Students;
        }
        public void Add(Student student)
        {
             _dbContext.Students.Add(student);
          
        }
        public void Update(Student student)
        {
            _dbContext.Students.Update(student);
          
        }
        public void Delete(int id)
        {
            var student = _dbContext.Students.Find(id);
            if (student != null)
            {
                _dbContext.Students.Remove(student);
               
            }
        }

        
        
    }
}
