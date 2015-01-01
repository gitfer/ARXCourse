using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MyOwinHost
{
    public class MyTracingComponent
    {
            private readonly Func<IDictionary<string, object>, System.Threading.Tasks.Task> next;
            private readonly int _requestCounter;

        // Additional parameters available after next...
        public MyTracingComponent(Func<IDictionary<string, object>, System.Threading.Tasks.Task> next)
            {
                this.next = next;
            }

        public System.Threading.Tasks.Task Invoke(IDictionary<string, object> env)
            {
                var requestPath = (string)env["owin.RequestPath"];

                // ...here do something meaningful. Like tracing...
                if(requestPath == "/")
                    env.Add("myDemo.MyTracingComponent", "Hello from tracing component! Try some request different than root!");

                // We must pass-through onto the next module
                return this.next(env);
            }
    }
}