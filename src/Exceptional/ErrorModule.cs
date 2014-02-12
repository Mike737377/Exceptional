using NLog;
using System;
using System.Web;

namespace Exceptional
{
    public class ErrorModule : IHttpModule
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

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
            // Below is an example of how you can handle LogRequest event and provide
            // custom logging implementation for it
            context.LogRequest += new EventHandler(OnLogRequest);
            context.Error += Error;
        }

        private void Error(object sender, EventArgs e)
        {
            Error(GetHttpContext());
        }

        private void Error(HttpContextBase httpContext)
        {
            var exception = httpContext.Error;
            if (exception != null)
            {
                log.Error(exception);
            }
        }

        private HttpContextBase GetHttpContext()
        {
            return new HttpContextWrapper(HttpContext.Current);
        }

        #endregion IHttpModule Members

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }
    }
}