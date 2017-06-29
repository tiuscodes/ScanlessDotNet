using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scanless.Scanners
{
    public class yougetsignal : ScanScript<yougetsignal>, IRequest
    {
        string targethost { get; set; }

        public yougetsignal(string targethost)
        {
            this.targethost = targethost;
        }

        public RequestInfo GetRequestInfo()
        {
            var values = new Dictionary<string, string>();
            values.Add("remoteAddress", this.targethost);
            return new RequestInfo()
            {
                ENDPOINT = "http://ports.yougetsignal.com",
                Script = "/short-scan.php",
                Method = HTTPMETHOD.POST,
                Values = values
            };
        }

        public override void SetResponse(string result)
        {
            //TODO: Process results
            base.SetResponse(result);
        }
    }
}
