﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApp
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class sail_heEntities : DbContext
    {
        public sail_heEntities()
            : base("name=sail_heEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<basic_city> basic_city { get; set; }
        public virtual DbSet<basic_province> basic_province { get; set; }
        public virtual DbSet<sys_enterprise> sys_enterprise { get; set; }
        public virtual DbSet<sys_user> sys_user { get; set; }
        public virtual DbSet<ucourse> ucourse { get; set; }
        public virtual DbSet<udept> udept { get; set; }
        public virtual DbSet<ugrade> ugrade { get; set; }
        public virtual DbSet<ujobtable> ujobtable { get; set; }
        public virtual DbSet<usc> usc { get; set; }
        public virtual DbSet<ustudent> ustudent { get; set; }
        public virtual DbSet<uteacher> uteacher { get; set; }
    }
}