using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
namespace Jagua.Clases
{
    public class Logger
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static ILog Log
        {
            get
            {
                InicializarLogging();
                return log;
            }
        }

        public static void InicializarLogging()
        {
            if (!log4net.LogManager.GetRepository().Configured)
                log4net.Config.XmlConfigurator.Configure();
        }
    }
}