namespace Pattern_Memento.Domain
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class CanvasStateContext : DbContext
    {
        public CanvasStateContext()
            : base("name=CanvasStateContext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CanvasStateContext>());
        }

        public DbSet<Canvas> Canvases { get; set; }
        public DbSet<Figure> Figures { get; set; }
        public virtual DbSet<Circle> Circles { get; set; }
        public virtual DbSet<Rectangle> Rectangles { get; set; }

    }
}