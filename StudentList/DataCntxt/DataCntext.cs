using Microsoft.EntityFrameworkCore;
using StudentList.Models;

namespace StudentList.DataCntxt
{
    public class DataCntext : DbContext
    {
        public DataCntext(DbContextOptions<DataCntext> options) : base(options)
        {

        }


        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }


    }
}
