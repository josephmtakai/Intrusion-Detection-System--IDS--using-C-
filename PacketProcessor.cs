using System;
using PcapDotNet.Core;
using PcapDotNet.Packets;

namespace IDS
{
    public class PacketProcessor
    {
        private AlertManager _alertManager;

        public PacketProcessor()
        {
            _alertManager = new AlertManager();
        }

        public void ProcessPacket(Packet packet)
        {
            // Analyze packet (for simplicity, only basic checks are shown here)
            if (packet.Ethernet.IpV4 != null)
            {
                var ipPacket = packet.Ethernet.IpV4;
                
                // Example: Simple check for suspicious packets (e.g., abnormal source port)
                if (ipPacket.Tcp != null && ipPacket.Tcp.SourcePort == 1234)
                {
                    _alertManager.SendAlert("Suspicious packet detected from source port 1234.");
                }
            }
        }
    }
}
