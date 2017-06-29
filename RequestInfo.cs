using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scanless
{
    public class RequestInfo
    {
        public string ENDPOINT { get; set; }
        public string Script { get; set; }
        public HTTPMETHOD Method { get; set; }
        public Dictionary<string, string> Values { get; set; }
    }
}
