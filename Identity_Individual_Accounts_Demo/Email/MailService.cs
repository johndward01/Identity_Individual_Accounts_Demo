using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Identity_Individual_Accounts_Demo.Email
{
    public class MailService
    {
        // the domain name you have verified in your Mailgun account
        const string DOMAIN = "example.com";

        // your API Key used to send mail through the Mailgun API
        const string API_KEY = "key-00000000000000000000000000000000";

        public async Task<bool> SendAsync(string from, string to, string subject, string message)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes("api" + ":" + API_KEY)));

            var form = new Dictionary<string, string>
            {
                ["from"] = from,
                ["to"] = to,
                ["subject"] = subject,
                ["text"] = message
            };

            var response = await client.PostAsync("https://api.mailgun.net/v2/" + DOMAIN + "/messages", new FormUrlEncodedContent(form));

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Debug.WriteLine("Success");
                return true;
            }
            else
            {
                Debug.WriteLine("StatusCode: " + response.StatusCode);
                Debug.WriteLine("ReasonPhrase: " + response.ReasonPhrase);
                return false;
            }
        }
    }
}
