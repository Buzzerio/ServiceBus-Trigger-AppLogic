namespace FrameworkTest
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TestTriggerContext : DbContext
    {
        public TestTriggerContext()
            : base("name=UAB")
        {
        }

        public virtual DbSet<Test_Trigger> Test_Trigger { get; set; }

        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test_Trigger>()
                .Property(e => e.campo)
                .IsFixedLength();
        }
    }
}
