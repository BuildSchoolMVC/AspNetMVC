using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;

namespace AspNetMVC.Models
{
    public class GoogleReCaptcha
    {
        public bool Valid { get; set; }
        public bool GetCaptchaResponse(string message)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create
            ("https://www.google.com/recaptcha/api/siteverify?secret=6LeLVlMaAAAAAFOFmNiKpOaixgUm8x4KihVkwCkp&response=" + message);
            try
            {
                using (WebResponse wResponse = req.GetResponse())
                {
                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        string jsonResponse = readStream.ReadToEnd();

                        JavaScriptSerializer js = new JavaScriptSerializer();
                        Result data = js.Deserialize<Result>(jsonResponse);

                        Valid = Convert.ToBoolean(data.Success);
                    }
                }

                return Valid;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }
        public class Result
        {
            public string Success { get; set; }
        }

    }
}