using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace PingPongBo
{
     class PingPongApi
    {
        private string ApiKey { get; set; }
        private string ApiUrl { get; set; }

         public PingPongApi(string apiKey, string apiUrl)
         {
             ApiKey = apiKey;
             ApiUrl = apiUrl;
         }

         public List<PingPongEvent> GetEvents(int offset,int limit)
         {
             var url = $"/events/events-sorted-by-name?apiKey={ApiKey}&offset={offset}&limit={limit}";
             //Console.Out.WriteLine(url);
             //Console.ReadKey();
             var data = httpGet(url);
            var events = JsonConvert.DeserializeObject<List<PingPongEvent>>(data);
             return events;
         }

         public EventAndEnabledFunctions GetEventAndEnabledFunctions(int eventId)
         {
             var data =
                 httpGet($"events/event-and-enabled-functions?apiKey={ApiKey}&eventId={eventId}&language=en");
            
            return JsonConvert.DeserializeObject<EventAndEnabledFunctions>(data);
        }

         public PingPongDocumentResourceOrFolder GetPongDocumentResourceOrFolder(int eventId)
         {
             var data = httpGet($"documents/event-folder?apiKey={ApiKey}&eventId={eventId}");
            var converter = new PingPongDocumnetConverter();
            var deserializedArray = JsonConvert.DeserializeObject<PingPongDocumentFolder>(
                data,
                converter);
            return deserializedArray;

        }

        

         private string httpGet(string url)
         {
            var request = WebRequest.Create($"{ApiUrl}/api/{url}");
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;

            // Get the response.

            var response = request.GetResponse();
            // Display the status.
            // Get the stream containing content returned by the server.
            var dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            var reader = new StreamReader(dataStream);
            // Read the content.
            var responseFromServer = reader.ReadToEnd();
            // Display the content.
            //Console.WriteLine(responseFromServer);
            // Clean up the streams and the response.
            reader.Close();
            response.Close();
             return responseFromServer;
         }
    }
}
