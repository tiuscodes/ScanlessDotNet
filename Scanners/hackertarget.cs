using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scanless
{
    public class hackertarget : ScanScript<hackertarget>, IRequest
    {
        string targethost { get; set; }
        public hackertarget(string targethost)
        {
            this.targethost = targethost;
        }

        public RequestInfo GetRequestInfo()
        {

            var values = new Dictionary<string, string>();
            values.Add("theinput", this.targethost);
            values.Add("thetest", "nmap");
            values.Add("name_of_nonce_field", "5a8d0006b9");
            values.Add("_wp_http_referer", "/nmap-online-port-scanner/");

            return new RequestInfo()
            {
                ENDPOINT = "https://hackertarget.com",
                Script = "/nmap-online-port-scanner/",
                Method = HTTPMETHOD.POST,
                Values = values
            };
        }

        public override void SetResponse(string result)
        {
            base.SetResponse(result);
        }
    }
}
