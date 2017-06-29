using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scanless.Scanners
{
    public class t1shopper: ScanScript<t1shopper>, IRequest
    {
        string targethost { get; set; }
        int[] scanports { get; set; }
        public t1shopper(string targethost)
        {
            this.targethost = targethost;
            //scan ranges seem arbitrary
            this.scanports = new int[] { 21, 23, 25, 80, 110, 139, 445, 1433, 1521, 1723, 3306, 3389, 5900, 8080 };
        }

        public RequestInfo GetRequestInfo()
        {
            var values = new Dictionary<string, string>();
            values.Add("scan_host", this.targethost);
            
            if(this.scanports == null)
            {
                throw new Exception("no ports specified");
            }
            foreach(int p in this.scanports)
            {
                values.Add("port_array[]", p.ToString());
            }

            return new RequestInfo()
            {
                ENDPOINT = "http://www.t1shopper.com",
                Script = "/tools/port-scan/result/",
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
