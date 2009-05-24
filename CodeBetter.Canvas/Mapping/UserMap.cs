namespace CodeBetter.Canvas.Mapping
{
    using FluentNHibernate.Mapping;
    
    public class UserMap : ClassMap<User>  
    {
        public UserMap()
        {
            Id(x => x.Id);
            Map(x => x.Name).Not.Nullable().WithLengthOf(30);
            Component(x => x.Credentials, credentials =>
            {
                credentials.Map(c => c.Email).Not.Nullable().WithLengthOf(50);
                credentials.Map(c => c.Password).Not.Nullable().WithLengthOf(100);
            });            
            Not.LazyLoad();
        }
    }
}