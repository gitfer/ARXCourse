using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MyOwinHost
{
    public class MyAutenticationComponent
    {
            private readonly Func<IDictionary<string, object>, System.Threading.Tasks.Task> next;
        private readonly string _username;
        private readonly string _password;
        private readonly int _requestCounter;

            public MyAutenticationComponent(Func<IDictionary<string, object>, System.Threading.Tasks.Task> next, string username, string password)
            {
                this.next = next;
                _username = username;
                _password = password;
            }

        public System.Threading.Tasks.Task Invoke(IDictionary<string, object> env)
            {

                if (_username.ToUpper() != "FEDE" || _password != "123")
                    throw new Exception("User not autenticated");
                // We must pass-through onto the next module
                return this.next(env);
            }
    }
}