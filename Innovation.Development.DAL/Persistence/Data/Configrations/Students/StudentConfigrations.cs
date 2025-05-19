using Innovation.Development.DAL.Entities.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovation.Development.DAL.Persistence.Data.Configrations.Students
{
    internal class StudentConfigrations : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(d => d.Id).UseIdentityColumn();
            builder.Property(n=> n.Name)
                .HasMaxLength(100);
        }
    }
}
