using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;

namespace MyKatanaConsole
{
    public static class MiddlewareComponentsExtensions
    {
            public static void UseMyAutentication(this IAppBuilder appBuilder, object[] parametri)
            {
                appBuilder.Use<MyAutenticationComponent>(parametri);
            }
    }
}