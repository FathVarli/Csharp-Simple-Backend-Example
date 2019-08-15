using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using DevFramework.Northwind.Entities.Concrete;

namespace DevFramework.Northwind.DataAccess.Concrete.EntityFramework.Mappings
{
    public class UserRoleMap : EntityTypeConfiguration<UserRole>
    {
        public UserRoleMap()
        {
            ToTable(@"UserRoles", @"dbo");
            HasKey(x => x.Id);

            Property(x => x.RoleId).HasColumnName("RoleId");
            Property(x => x.UserId).HasColumnName("UserId");
        }
    }
}
