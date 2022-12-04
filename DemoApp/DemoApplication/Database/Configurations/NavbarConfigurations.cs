using DemoApplication.Database.Models;
using DemoApplication.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApplication.Database.Configurations
{
    public class NavbarConfigurations: IEntityTypeConfiguration<NavbarM>
    {
        public void Configure(EntityTypeBuilder<NavbarM> builder)
        {
            builder
               .ToTable("Navbars");
        }
    }
}
