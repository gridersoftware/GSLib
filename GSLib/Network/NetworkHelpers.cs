using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace GSLib.Network
{
    public static class NetworkHelpers
    {
        /// <summary>
        /// Gets the local IP address of the system.
        /// </summary>
        /// <returns>Returns an IP address that represents the local system.</returns>
        public static IPAddress GetLocalAddress()
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                GatewayIPAddressInformation address = ni.GetIPProperties().GatewayAddresses.FirstOrDefault();
                if (address != null && address.Address.ToString() != "0.0.0.0")
                {
                    if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 |
                        ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet |
                        ni.NetworkInterfaceType == NetworkInterfaceType.GigabitEthernet)
                    {
                        foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                return ip.Address;
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}
