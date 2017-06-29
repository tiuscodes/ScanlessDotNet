using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Specialized;
using System.Threading;

namespace Scanless
{
    public class Request
    {
        public Request()
        {

        }

        /// <summary>
        /// Send a request (duh)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sender"></param>
        /// <returns></returns>
        protected async Task SendAsync<T>(T sender) where T : IRequest
        {
            var ri = sender.GetRequestInfo();
            string URI = ri.ENDPOINT + ri.Script;
            var c = new WebClient();
            

            if (ri.Method == HTTPMETHOD.GET)
            {
                var append = String.Join("&", ri.Values.Select(kv => kv.Key + "=" + kv.Value));
                string requesturi = String.Concat(URI, "?", append);
                var result = await c.DownloadStringTaskAsync(requesturi);
                sender.SetResponse(result);
            }
            else
            {
                //POST
                var result = await c.UploadValuesTaskAsync(new Uri(URI), ri.Method.ToString(), ri.Values.ToNameValueCollection());
                sender.SetResponse(Encoding.UTF8.GetString(result));
            }
        }

        /// <summary>
        /// Poll a request until the specified string is found in the response
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sender"></param>
        /// <param name="ri"></param>
        /// <param name="untilexists"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="pollintervalsecs"></param>
        /// <returns></returns>
        protected async Task PollAsync<T>(T sender, RequestInfo ri, string untilexists, CancellationToken cancellationToken, int pollintervalsecs = 30) where T : IRequest
        {
            string URI = ri.ENDPOINT + ri.Script;
            var c = new WebClient();
            await Task.Run(async () =>
            {
                string httpresult = "";
                do
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    if (ri.Method == HTTPMETHOD.GET)
                    {
                        var append = String.Join("&", ri.Values.Select(kv => kv.Key + "=" + kv.Value));
                        string requesturi = String.Concat(URI, "?", append);
                        var result = await c.DownloadStringTaskAsync(requesturi);
                        httpresult = (result);
                    }
                    else
                    {
                        //POST
                        var result = await c.UploadValuesTaskAsync(new Uri(URI), ri.Method.ToString(), ri.Values.ToNameValueCollection());
                        httpresult = (Encoding.UTF8.GetString(result));
                    }
                    await Task.Delay(new TimeSpan(0, 0, pollintervalsecs));
                } while (!httpresult.Contains(untilexists));//Poll until specified text is found in the resulting page
                sender.SetResponse(httpresult);
            }, cancellationToken);

        }

    }

    public enum HTTPMETHOD
    {
        GET,
        POST
    }

    
}
