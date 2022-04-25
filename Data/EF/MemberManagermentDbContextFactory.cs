
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;

//namespace CompatriotsClub.Data.EF
//{
//    public class MemberManagermentDbContextFactory : IDesignTimeDbContextFactory<CompatriotsClubContext>
//    {
//        public CompatriotsClubContext CreateDbContext(string[] args)
//        {
//            IConfigurationRoot configuration = new ConfigurationBuilder()
//               .SetBasePath(Directory.GetCurrentDirectory())
//               .AddJsonFile("appsettings.json")
//               .Build();

//            var connectionString = configuration.GetConnectionString("MemberManagementConnext");

//            var optionsBuilder = new DbContextOptionsBuilder<CompatriotsClubContext>();
//            optionsBuilder.UseSqlServer(connectionString);

//            return new CompatriotsClubContext(optionsBuilder.Options);
//        }
//    }
//}
