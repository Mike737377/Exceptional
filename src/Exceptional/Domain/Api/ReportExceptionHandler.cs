using Exceptional.Infrastructure;
using Exceptional.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptional.Domain.Api
{
    public class ReportExceptionHandler : IHandler<ReportException>
    {
        private readonly IDatabase database;

        public ReportExceptionHandler(IDatabase database)
        {
            this.database = database;
        }

        public void Execute(ReportException message)
        {
            var application = database.Query<Model.Application>().Where(new { ApiKey = message.ApiKey }).FirstOrDefault();

            var exceptionInfo = Mapper.Map(message.Report).To<ExceptionGroup>();
            var exceptionDetail = Mapper.Map(message.Details).To<ExceptionInstance>();
            var applicationUser = new ApplicationUser()
            {
                ApplicationId = application.ApplicationId,
                UserName = message.Report.UserName
            };

            var existingUser = database.Query<ApplicationUser>().Where(applicationUser).FirstOrDefault();

            if (existingUser == null)
            {
                database.Insert(applicationUser);
                existingUser = applicationUser;
            }

            database.Insert(exceptionInfo);
            database.Insert(exceptionDetail);

            SaveExceptionInstanceState(exceptionInfo.ExceptionGroupId, ExceptionInstanceStateType.Cookie, message.Report.Cookies);
            SaveExceptionInstanceState(exceptionInfo.ApplicationId, ExceptionInstanceStateType.Form, message.Report.PostData);
            SaveExceptionInstanceState(exceptionInfo.ApplicationId, ExceptionInstanceStateType.QueryString, message.Report.QueryData);
            SaveExceptionInstanceState(exceptionInfo.ApplicationId, ExceptionInstanceStateType.Server, message.Report.ServerVariable);
        }

        private void SaveExceptionInstanceState(Guid exceptionInstanceId, ExceptionInstanceStateType stateType, string[][] stateData)
        {
            stateData.Each(v =>
                {
                    var state = new ExceptionInstanceState()
                    {
                        ExceptionInstanceId = exceptionInstanceId,
                        StateType = stateType,
                        Key = v[0],
                        Value = v[1]
                    };

                    database.Insert(state);
                });
        }
    }
}