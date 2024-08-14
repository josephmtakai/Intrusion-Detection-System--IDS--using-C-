using System;
using System.Threading;
using PcapDotNet.Core;
using PcapDotNet.Core.Extensions;

namespace IDS
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize PacketProcessor and AlertManager
            PacketProcessor packetProcessor = new PacketProcessor();
            AlertManager alertManager = new AlertManager();

            // Get all network devices
            var devices = LivePacketDevice.AllLocalMachine;
            if (devices.Count == 0)
            {
                Console.WriteLine("No network devices found.");
                return;
            }

            // Select the first available device
            PacketDevice selectedDevice = devices[0];

            // Open the device for packet capturing
            using (var communicator = selectedDevice.Open(65536, PacketDeviceOpenAttributes.Promiscuous, 1000))
            {
                Console.WriteLine($"Listening on {selectedDevice.Description}...");
                
                // Start packet capture
                communicator.OnPacketArrival += (sender, e) =>
                {
                    packetProcessor.ProcessPacket(e.Packet);
                };
                
                communicator.StartCapture();

                // Keep the application running
                Console.WriteLine("Press any key to stop...");
                Console.ReadKey();
            }
        }
    }
}
