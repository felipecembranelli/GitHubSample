using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiHubSample.Web.App_Start
{
    public static class Bootstrapper
    {
        public static void Run()
        {

            // IOC configuration
            AutoFacConfig.ConfigureContainer();
        }

    }
}