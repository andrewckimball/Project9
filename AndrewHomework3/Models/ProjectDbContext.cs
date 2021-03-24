using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewHomework3.Models
{
    public class ProjectDbContext: DbContext
    {
        //Constructor when instance of object is built the first time
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {

        }

        public DbSet<MovieModel> ProjectModel { get; set; }
    }
}
