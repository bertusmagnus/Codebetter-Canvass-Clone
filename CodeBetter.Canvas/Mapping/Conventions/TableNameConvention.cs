namespace CodeBetter.Canvas.Mapping.Conventions
{
    using FluentNHibernate.Conventions;
    using FluentNHibernate.Mapping;

    public class TableNameConvention : IClassConvention
    {
        public bool Accept(IClassMap target)
        {
            return string.IsNullOrEmpty(target.TableName);
        }

        public void Apply(IClassMap target)
        {
            target.WithTable(target.EntityType.Name + "s");
        }
    }
}