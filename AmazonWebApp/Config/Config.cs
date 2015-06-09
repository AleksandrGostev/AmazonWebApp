using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmazonWebApp
{
    public static class Config
    {
        public static string MyAwsSecretKeyId = System.Configuration.ConfigurationManager.AppSettings["MyAwsSecretKeyId"];
        public static string MyAwsSecretKey = System.Configuration.ConfigurationManager.AppSettings["MyAwsSecretKey"];
        public static string Destination = System.Configuration.ConfigurationManager.AppSettings["Destination"];
        public static string Namespace = System.Configuration.ConfigurationManager.AppSettings["Namespace"];
    }
}