using Innovation.Development.DAL.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovation.Development.DAL.Entities.Students
{
    public class Subject : BaseEntity<int>
    {
        public required string Name { get; set; }

        public ICollection<Student> Students { get; set; }= new List<Student>();
    }
}
