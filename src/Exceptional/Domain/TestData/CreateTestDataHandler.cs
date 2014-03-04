using Exceptional.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Domain.TestData
{
    public class CreateTestDataHandler : IHandler<CreateTestData>
    {
        private readonly IDatabase database;

        public CreateTestDataHandler(IDatabase database)
        {
            this.database = database;
        }

        public void Execute(CreateTestData message)
        {
            throw new NotImplementedException();
        }
    }
}