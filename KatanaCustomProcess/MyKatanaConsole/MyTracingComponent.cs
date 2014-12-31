using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MyKatanaConsole
{
    public class MyTracingComponent
    {
            private readonly Func<IDictionary<string, object>, Task> next;
            private readonly int _requestCounter;

        // Additional parameters available after next...
        public MyTracingComponent(Func<IDictionary<string, object>, Task> next)
            {
                this.next = next;
            }

        public Task Invoke(IDictionary<string, object> env)
            {
                var requestPath = (string)env["owin.RequestPath"];

                Console.WriteLine("Requesting url: " + requestPath);
                Console.WriteLine("------------------------------------------");
                // ...here do something meaningful. Like tracing...
                if(requestPath == "/")
                    Console.WriteLine("Hello from tracing component! Try some request different than root!");

                // We must pass-through onto the next module
                return this.next(env);
            }
    }
}