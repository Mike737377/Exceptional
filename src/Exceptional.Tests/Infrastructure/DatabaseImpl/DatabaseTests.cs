using Exceptional.Infrastructure.DatabaseImpl;
using Exceptional.Model;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Exceptional.Tests.Infrastructure.DatabaseImpl
{
    public class DatabaseTests
    {
        private readonly Database database = new Database();
        private readonly Fixture autoFixture = new Fixture();

        public class TestEntity
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime Date { get; set; }
        }

        [Fact]
        public void Insert_Query_Count_Update_Query_Delete()
        {
            var user = autoFixture.Create<User>();
            database.Insert(user);

            Console.WriteLine(database.Query<User>().ToArray().ToJsonString());
            Console.WriteLine("Count = {0}", database.Query<User>().Count());

            user.Name = "New NAME!";
            database.Update(user);

            Console.WriteLine(database.Query<User>()
                    .Where(new User() { UserId = user.UserId })
                    .SortBy(x => new[] { x.Name })
                    .FirstOrDefault().ToJsonString());

            database.Delete(user);
        }
    }
}