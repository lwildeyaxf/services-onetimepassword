using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace services_onetimepassword
{
    public class OTPService
    {
        //public async Task<ResponseMLAReport> Post(ApplicationSettings settings, Customer customer)
        //{
        //    NetConnectRequest ncRequest = new NetConnectRequest(settings, customer);
        //    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        //    ns.Add("", "");
        //    string xml = "&NETCONNECT_TRANSACTION=";
        //    using (StringWriter sw = new StringWriter())
        //    {
        //        using (XmlWriter xm = XmlWriter.Create(sw))
        //        {
        //            XmlSerializer x = new XmlSerializer(ncRequest.GetType());
        //            x.Serialize(sw, ncRequest, ns);
        //        }
        //        xml += WebUtility.UrlEncode(sw.ToString());
        //    }

        //    //send the request
        //    try
        //    {
        //        string netConnectURl = GetAPIURL(settings.URL);
        //        string response = await CallAPI(settings.Username, settings.Password, netConnectURl, xml);
        //        var ncResponse = ParseResponse(response);
        //        return (ncResponse.Products.MLAReport);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

        public async Task<string> GenerateOTP(string username, string password, string url, string phoneNumber)
        {
            using (var client = new HttpClient())
            {
                //client.Timeout = new TimeSpan(1000000);
                //client.DefaultRequestHeaders.Add("Connection", "close");
                var byteArray = new UTF8Encoding().GetBytes(username + ":" + password);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("BASIC", Convert.ToBase64String(byteArray));

                var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
                //request.Content = new ByteArrayContent(Encoding.ASCII.GetBytes(xml));
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                var result = await client.SendAsync(request);
                result.EnsureSuccessStatusCode();
                return await result.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> VerifyOTP(string username, string password, string url, string sessionID, string code)
        {
            using (var client = new HttpClient())
            {
                //client.Timeout = new TimeSpan(1000000);
                //client.DefaultRequestHeaders.Add("Connection", "close");
                var byteArray = new UTF8Encoding().GetBytes(username + ":" + password);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("BASIC", Convert.ToBase64String(byteArray));

                var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
                //request.Content = new ByteArrayContent(Encoding.ASCII.GetBytes(xml));
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                var result = await client.SendAsync(request);
                result.EnsureSuccessStatusCode();
                return await result.Content.ReadAsStringAsync();
            }
        }

        private static string ConvertToBase64String(string input)
        {
            byte[] info = Encoding.ASCII.GetBytes(input);
            //Convert the binary input into base 64 UUEncode output. //Each 3 byte sequence in the source data becomes a 4 byte //sequence in the character array.
            long dataLength = (long)((4.0d / 3.0d) * info.Length);
            //if length is not divisible by 4, go up to the next multiple of 4.
            if (dataLength % 4 != 0) dataLength += 4 - dataLength % 4;
            char[] base64CharArray = new char[dataLength];
            Convert.ToBase64CharArray(info, 0, info.Length, base64CharArray, 0);
            return new string(base64CharArray);
        }

    }
}
