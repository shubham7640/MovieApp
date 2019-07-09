using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace MovieApplication.Models
{
    public class MovieContext : DbContext
    {
        public DbSet<Movies> movie { get; set; }
        public DbSet<Actor> actor { get; set; }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Movies>()
        //        .HasMany<Actor>(s => s.Actors)
        //        .WithMany(c => c.Movies)
        //        .Map(cs =>
        //        {
        //            cs.MapLeftKey("MovieId");
        //            cs.MapRightKey("CastId");
        //            cs.ToTable("MoviesActor");
        //        });
//        }
    }
}