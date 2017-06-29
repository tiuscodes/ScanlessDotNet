using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scanless.Scanners
{
    public class portcheckers : ScanScript<portcheckers>, IRequest
    {
        string targethost { get; set; }

        public portcheckers(string targethost)
        {
            this.targethost = targethost;
        }

        public RequestInfo GetRequestInfo()
        {
            var values = new Dictionary<string, string>();
            values.Add("server", this.targethost);
            values.Add("quick", "true");
            return new RequestInfo()
            {
                ENDPOINT = "http://www.portcheckers.com",
                Script = "/portscan-result",
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
