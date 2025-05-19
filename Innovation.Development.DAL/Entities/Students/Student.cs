using Innovation.Development.DAL.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Innovation.Development.DAL.Entities.Students
{
    internal class Student : BaseEntity<int>
    {
        public required string Name { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public required string Address  { get; set; }

    }
}
