using System;
using System.IO;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace jadr
{
    class HttpServer
    {
        public static HttpListener listener;
        public static string url = "http://*:4935/";
        public static int pageViews = 0;
        public static int requestCount = 0;
        public static string pageData =
            "<!DOCTYPE>" +
            "<html>" +
            "  <head>" +
            "    <title>jadr</title>" +
            "  </head>" +
            "</html>";


        public static async Task HandleIncomingConnections()
        {
            bool runServer = true;

            // While a user hasn't visited the `shutdown` url, keep on handling requests
            while (runServer)
            {
                // Will wait here until we hear from a connection
                HttpListenerContext ctx = await listener.GetContextAsync();

                // Peel out the requests and response objects
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

                // Print event
                Console.WriteLine("Event #: {0}", ++requestCount);
                ShowRequestData(req);

                Console.WriteLine();

                // Write the response info
                string disableSubmit = !runServer ? "disabled" : "";
                byte[] data = Encoding.UTF8.GetBytes(String.Format(pageData, pageViews, disableSubmit));
                resp.ContentType = "text/html";
                resp.ContentEncoding = Encoding.UTF8;
                resp.ContentLength64 = data.LongLength;

                // Write out to the response stream (asynchronously), then close it
                await resp.OutputStream.WriteAsync(data, 0, data.Length);
                resp.Close();
            }
        }


        public static void Main(string[] args)
        {
            // Create a Http server and start listening for incoming connections
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
           
            Console.WriteLine();
            Console.WriteLine("##########################");
            Console.WriteLine("#                        #");
            Console.WriteLine("#          jadr          #");
            Console.WriteLine("#                        #");
            Console.WriteLine("##########################");
            Console.WriteLine("# Version: 0.1.0");
            Console.WriteLine("# Listener is running on: " + "{0}", url.Replace("*", GetLocalIPv4(NetworkInterfaceType.Wireless80211))); // Wireless ip address only
            Console.WriteLine("# NOTE: Please activate accessibility service in your phone settings.");
            Console.WriteLine();

            // Handle requests
            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();

            // Close the listener
            listener.Close();
        }

        public static string GetRequestBody(HttpListenerRequest req)
        {
            var bodyStream = new StreamReader(req.InputStream);
            bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
            var bodyText = bodyStream.ReadToEnd();
            return bodyText;
        }

        public static void ShowRequestData(HttpListenerRequest request)
        {
            if (!request.HasEntityBody)
            {
                return;
            }
            System.IO.Stream body = request.InputStream;
            System.Text.Encoding encoding = request.ContentEncoding;
            System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);

            string s = reader.ReadToEnd();
            Console.WriteLine(s);
            println(s);
            body.Close();
            reader.Close();
        }

        public static string GetLocalIPv4(NetworkInterfaceType _type)
        {
            string output = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
            return output;
        }

        public static void println(String Message)
        {
#if DEBUG
            Console.WriteLine("[" + DateTime.Now + "]" + " " + Message);
#endif

            try
            {
                String dataPath = AppDomain.CurrentDomain.BaseDirectory;

                if (!Directory.Exists(dataPath))
                {
                    Directory.CreateDirectory(dataPath);
                }

                // Logfile
                dataPath = Path.Combine(dataPath, "log");

                if (!Directory.Exists(dataPath))
                {
                    Directory.CreateDirectory(dataPath);
                }

                if (!File.Exists(Path.Combine(dataPath, "log.txt")))
                {
                    using (File.Create(Path.Combine(dataPath, "log.txt")))
                    { };
                }

                File.AppendAllText(Path.Combine(dataPath, "log.txt"), Message + Environment.NewLine);
            }
#pragma warning disable CS0168
            catch (Exception ex)
#pragma warning restore CS0168
            {
#if DEBUG
                Console.WriteLine("[" + DateTime.Now + "]" + " " + ex.ToString());
#endif
            }

        }
    }
}