﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FolderAnalysis
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class edmFolderAnalysisContainer : DbContext
    {
        public edmFolderAnalysisContainer()
            : base("name=edmFolderAnalysisContainer")
        {
					//Database.SetInitializer<edmFolderAnalysisContainer>(new DropCreateDatabaseAlways<edmFolderAnalysisContainer>());
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<files> files { get; set; }
        public DbSet<session> sessions { get; set; }
        public DbSet<attribute> attributes { get; set; }
    }
}
