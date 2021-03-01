using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace jadr_server
{
    class Program
    {
        static void Main(string[] args)
        {
            // jadr-agent
            using (File.Create("jadr-server.txt")) { };
            File.WriteAllText("jadr-server.txt", "http://" + GetLocalIPv4(NetworkInterfaceType.Wireless80211) + ":4935/");
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
    }
}
