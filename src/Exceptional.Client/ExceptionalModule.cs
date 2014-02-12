using System;
using System.Web;

namespace Exceptional.Client
{
    public class ExceptionalModule : IHttpModule
    {
        /// <summary>
        /// You will need to configure this module in the Web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>

        #region IHttpModule Members

        public void Dispose()
        {
            //clean-up code here.
        }

        public void Init(HttpApplication context)
        {
            context.Error += Error;
        }

        public void Error(object sender, EventArgs e)
        {
            Error(GetHttpContext());
        }

        private HttpContextBase GetHttpContext()
        {
            return new HttpContextWrapper(HttpContext.Current);
        }

        public void Error(HttpContextBase httpContext)
        {
            var exceptional = new ExceptionalApi();
            exceptional.ReportException(httpContext.Error);
        }

        #endregion IHttpModule Members
    }
}