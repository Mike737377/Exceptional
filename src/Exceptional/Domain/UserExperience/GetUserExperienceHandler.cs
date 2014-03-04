using Exceptional.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Domain.UserExperience
{
    public class GetUserExperienceHandler : IHandler<GetUserExperience>
    {
        private readonly IDatabase database;

        public GetUserExperienceHandler(IDatabase database)
        {
            this.database = database;
        }

        public void Execute(GetUserExperience message)
        {
            throw new NotImplementedException();
        }
    }
}