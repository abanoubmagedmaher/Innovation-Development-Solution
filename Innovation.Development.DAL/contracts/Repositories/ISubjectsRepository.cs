﻿using Innovation.Development.DAL.Entities.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovation.Development.DAL.contracts.Repositories
{
    public interface ISubjectsRepository
    {
        IEnumerable<Subject> GetAll(bool withTracking = false);
        Subject? Get(int id);
    }
}
