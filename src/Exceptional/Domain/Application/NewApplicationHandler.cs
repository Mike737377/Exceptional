using Exceptional.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Domain.Application
{
    public class NewApplicationHandler : IHandler<NewApplication>
    {
        private readonly IDatabase database;

        public NewApplicationHandler(IDatabase database)
        {
            this.database = database;
        }

        public void Execute(NewApplication message)
        {
            var app = Mapper.Map(message).To<Model.Application>();
            app.ApplicationId = GuidHelper.GenerateComb();

            database.Insert(app);
        }
    }
}