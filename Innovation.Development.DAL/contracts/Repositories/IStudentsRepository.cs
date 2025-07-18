﻿using Innovation.Development.DAL.Entities.Students;
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
        void  Add(Student student);
        void  Update(Student student);
        void  Delete(int id);
    }
}
