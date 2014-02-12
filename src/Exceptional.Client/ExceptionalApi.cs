using Exceptional.Client.Messages;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Exceptional.Client
{
    public class ExceptionalApi
    {
        private readonly RestClient client;

        public ExceptionalApi()
        {
            var baseUrl = string.Format(@"{0}/api/{1}", ExceptionalConfiguration.Config.Url, ExceptionalConfiguration.Config.ApiKey);
            this.client = new RestClient(baseUrl);
        }

        public Guid ReportException(Exception exception)
        {
            var report = exception.BuildExceptionReport();
            var request = new RestRequest("report", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(report);
            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ExceptionalClientException("Error occurred while attempting to report exception", response.ErrorException);
            }

            return report.ExceptionInstanceId;
        }
    }
}