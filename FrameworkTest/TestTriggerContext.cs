namespace FrameworkTest
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Core.Objects;

    public partial class TestTriggerContext : DbContext
    {
        public TestTriggerContext()
            : base("name=UAB")
        {
        }

        public virtual DbSet<Test_Trigger> Test_Trigger { get; set; }

        //public void SP()
        //{
        //    var campo = new ObjectParameter("@campo", "TestDaVS6");
        //    var valore = new ObjectParameter("@valore", 33);
        //    var RC = new ObjectParameter("@RC", 0);
        //    base.ExecuteFunction<Valuation>
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test_Trigger>()
                .Property(e => e.campo)
                .IsFixedLength();
        }
    }
}
