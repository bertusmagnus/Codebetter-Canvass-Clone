namespace CodeBetter.Canvas
{
    public class Entity<T> : FluentNHibernate.Data.Entity
    {
        public new T Id { get; set; }
    }
}