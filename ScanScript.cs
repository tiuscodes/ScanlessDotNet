using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scanless
{   
    //A port of the python scripts created by @vesche on github.com
    //See below:
    //https://github.com/vesche/scanless/tree/master/scanless/scanners

    public abstract class ScanScript<T> : Request where T : IRequest
    {
        public String Content { get; set; }
        //static ManualResetEvent eventprocessing = new ManualResetEvent(false);

        public async Task RunAsync()
        {
            await this.SendAsync<T>((T)Convert.ChangeType(this, typeof(T)));
        }

        public void Run()
        {
            this.SendAsync<T>((T)Convert.ChangeType(this, typeof(T))).Wait();
        }

        public virtual void SetResponse(string result)
        {
            //eventprocessing.Reset();
            this.Content = result;
        }
    }


}
