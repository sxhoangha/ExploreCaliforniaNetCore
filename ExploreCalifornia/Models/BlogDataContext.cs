﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreCalifornia.Models
{
    public class BlogDataContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Special> Specials { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public BlogDataContext(DbContextOptions<BlogDataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
