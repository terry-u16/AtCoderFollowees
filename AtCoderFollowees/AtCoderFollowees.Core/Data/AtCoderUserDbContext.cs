using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using AtCoderFollowees.Core.Models;

namespace AtCoderFollowees.Core.Data
{
    public class AtCoderUserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(user => user.TwitterID);   // 複数垢が同じscreen_nameを書いていると一意にならないので……
        }
    }
}
