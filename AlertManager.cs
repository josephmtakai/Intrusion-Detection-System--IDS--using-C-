using System;
using System.IO;

namespace IDS
{
    public class AlertManager
    {
        private readonly string _logFilePath;

        public AlertManager()
        {
            _logFilePath = "alerts.log";
        }

        public void SendAlert(string message)
        {
            // Output alert to console
            Console.WriteLine($"[ALERT] {message}");

            // Log alert to file
            LogAlert(message);
        }

        private void LogAlert(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(_logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to log alert: {ex.Message}");
            }
        }
    }
}
