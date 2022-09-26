using MkopaSMS.Consumer.Models;
using MkopaSMS.Consumer.RabbitMQConsumer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MkopaSMS.Consumer
{
    public  class WebSmsSender : IWebSmsSender
    {
        public Tuple<bool, string> Send(SmsPayloadFromRB message, string rawUrlString)
        {
            return PrepareFakeSend(rawUrlString, message);

            //For a wrapper around http request code below 

            //*****************************************************************************************
            //for real SMS gateway
            //HttpWebRequest request = PrepareSend(_rawUrlString, message);
            //StreamReader reader = null;
            //WebResponse response = null;
            //try
            //{
            //    response = request.GetResponse();

            //    // Logger.InfoFormat(Strings.GetString("WebRequestResponse"), uri);
            //    reader = new StreamReader(response.GetResponseStream(), _encoding);
            //    string status = reader.ReadToEnd();
            //    bool done = ParseStatus(status);
            //    return Tuple.Create<bool, string>(done, status);
            //}
            //finally
            //{
            //    if (response != null)
            //        response.Close();              //        Logging for SMS gateway  status
            //    if (reader != null)                //        Logging for SMS message delivery  status
            //        reader.Close();
            //}
            //*****************************************************************************************
        }

        private Tuple<bool, string> PrepareFakeSend(string rawUrlString, SmsPayloadFromRB message)
        {
            return Tuple.Create<bool, string>(true, "Fake SMS Sent successfuly: " + rawUrlString);
        }
        //protected abstract HttpWebRequest PrepareSend(string url, SmsPayload message);

        //protected abstract bool ParseStatus(string content);
    }
    
}
