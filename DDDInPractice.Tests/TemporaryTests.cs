using DDDInPractice.Logic;
using NHibernate;
using Xunit;

namespace DDDInPractice.Tests
{
    public class TemporaryTests
    {
        [Fact]
        public void Test()
        {
            SessionFactory.Init(@"Server=.\SQLEXPRESS;Database=DddInPractice;Trusted_Connection=true");

            using (ISession session = SessionFactory.OpenSession())
            {
                long id = 1;
                var snackMachine = session.Get<SnackMachine>(id);
            }
        }
    }
}