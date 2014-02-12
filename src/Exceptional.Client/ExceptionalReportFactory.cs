using Exceptional.Client.Messages;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security;
using System.Text;
using System.Web;

namespace Exceptional.Client
{
    public static class ExceptionalReportFactory
    {
        internal static string Mask(Func<string, bool> maskCallback, string key, string maskValue)
        {
            if (maskValue == null)
            {
                return null;
            }

            if (maskCallback == null)
            {
                return maskValue;
            }

            if (maskCallback(key))
            {
                return "####";
            }

            return maskValue;
        }

        internal static string[][] CreateCopy(NameValueCollection nameValueCollection, Func<string, bool> includeCallback = null)
        {
            if (includeCallback == null)
            {
                includeCallback = x => false;
            }

            return nameValueCollection.AllKeys
                    .Select(k => new string[] { k, Mask(includeCallback, k, nameValueCollection[k]) }).ToArray();
        }

        internal static string[][] CreateCopy(HttpCookieCollection cookieCollection, Func<string, bool> includeCallback = null)
        {
            if (includeCallback == null)
            {
                includeCallback = x => false;
            }

            return cookieCollection.AllKeys
                    .Select(c => new string[] { c, Mask(includeCallback, cookieCollection[c].Name, cookieCollection[c].Value) }).ToArray();
        }

        internal static string GetMachineName(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                try
                {
                    return System.Environment.MachineName;
                }
                catch (SecurityException)
                {
                }

                return null;
            }

            try
            {
                return httpContext.Server.MachineName;
            }
            catch (HttpException)
            {
            }
            catch (SecurityException)
            {
            }

            return null;
        }

        internal static ExceptionalReport BuildExceptionReport(this Exception exception)
        {
            var baseException = exception.GetBaseException();
            var report = new ExceptionalReport()
            {
                ExceptionInstanceId = Guid.NewGuid(),
                Exceptions = BuildExceptionList(baseException)
            };

            var httpContext = GetHttpContext();
            report.MachineName = GetMachineName(httpContext);
            report.UrlReferrer = httpContext.Request.UrlReferrer != null ? httpContext.Request.UrlReferrer.ToString() : null;
            report.Url = httpContext.Request.Url != null ? httpContext.Request.Url.ToString() : null;
            report.UserName = ExceptionalConfiguration.Config.GetUser();

            if (report.UserName == null)
            {
                if (httpContext.User != null && httpContext.User.Identity != null)
                {
                    report.UserName = httpContext.User.Identity.Name;
                }
            }

            var httpException = baseException as HttpException;
            if (httpException != null)
            {
                report.HttpCode = httpException.GetHttpCode();
                report.HtmlErrorMessage = httpException.GetHtmlErrorMessage();
            }

            try
            {
                report.QueryData = CreateCopy(httpContext.Request.QueryString, x => IsFilteredField(x));
                report.PostData = CreateCopy(httpContext.Request.Form, x => IsFilteredField(x));
                report.ServerVariable = CreateCopy(httpContext.Request.ServerVariables, x => IsFilteredField(x));
                report.Cookies = CreateCopy(httpContext.Request.Cookies, x => IsFilteredField(x));
            }
            catch (HttpRequestValidationException)
            { }

            return report;
        }

        public static bool IsFilteredField(string fieldName)
        {
            var fields = ExceptionalConfiguration.Config.FilteredFields;
            return fields.Contains(fieldName, StringComparer.OrdinalIgnoreCase);
        }

        private static ExceptionDetails[] BuildExceptionList(Exception baseException)
        {
            var exceptions = new List<ExceptionDetails>();
            var nextException = baseException;

            while (nextException != null)
            {
                var exceptionDetails = new ExceptionDetails()
                {
                    Message = nextException.Message,
                    StackTrace = nextException.StackTrace,
                    ExceptionType = nextException.GetType().FullName,
                    ExceptionHash = ComputeExceptionHash(nextException)
                };
                exceptions.Add(exceptionDetails);
                nextException = nextException.InnerException;
            }

            return exceptions.ToArray();
        }

        private static HttpContextBase GetHttpContext()
        {
            return new HttpContextWrapper(HttpContext.Current);
        }

        private static int ComputeExceptionHash(Exception exception)
        {
            return string.Format("{0}-{1}", ExceptionalConfiguration.Config.ApiKey, exception.ToString()).GetHashCode();
        }
    }
}