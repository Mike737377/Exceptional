using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Exceptional.Client
{
    public interface IExceptionalConfiguration
    {
        string ApiKey { get; }
        string Url { get; }
        
        string GetUser();

        string[] FilteredFields { get; }
    }

    public class ExceptionalConfigurationInstance : IExceptionalConfiguration
    {
        public string ApiKey
        {
            get
            {
                return ConfigurationManager.AppSettings["Exceptional-ApiKey"];
            }
        }

        public string Url
        {
            get
            {
                return ConfigurationManager.AppSettings["Exceptional-Url"];
            }
        }

        public string[] FilteredFields
        {
            get
            {
                var filteredFields = ConfigurationManager.AppSettings["Exceptional-FilteredFields"];
                if (string.IsNullOrWhiteSpace(filteredFields))
                {
                    return new string[] { };
                }
                return filteredFields.Split(new char[] { ',', ';' });
            }
        }

        public string GetUser()
        {
            return null;
        }
    }
}