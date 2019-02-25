using FluentNHibernate.Mapping;

namespace DDDInPractice.Logic.SnackMachines
{
    public class SnackMap : ClassMap<Snack>
    {
        public SnackMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
        }
    }
}