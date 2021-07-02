using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SerialPortComBridge
{
    /// <summary>
    /// Return a Serial Com Port  according to given configuration
    /// </summary>
    class ComPortConfig
    {
        /// <summary>
        /// Reads configuration and return port
        /// Config must be as : COMX-BAUD-SIZE-PARITY-STOP-[DTR]-[RTS] 
        /// Ex : COM1-9600-8-N-2 
        /// </summary>
        /// <returns></returns>
        public static SerialPort ConfigureSerialPort(string config)
        {
            if (String.IsNullOrEmpty(config)) return null;

            // Extract configuration from args
            string configPattern = @"([^-]*)";
            Regex rgx = new Regex(configPattern);


            SerialPort serial = new SerialPort();

            MatchCollection matches = rgx.Matches(config);
            if (matches.Count < 10) return null;

            serial.PortName = matches[0].Value;
            serial.BaudRate = Convert.ToInt32(matches[2].Value);
            serial.DataBits = Convert.ToInt16(matches[4].Value);

            var parities = Enum.GetValues(typeof(Parity));
            foreach (var p in parities)
            {
                if (p.ToString().Substring(0, 1).Equals(matches[6].Value))
                {
                    serial.Parity = (Parity)Enum.Parse(typeof(Parity), p.ToString());
                    break;
                }
            }

            serial.StopBits = (StopBits)Enum.Parse(typeof(StopBits), matches[8].Value);

            serial.RtsEnable = config.Contains("RTS");
            serial.DtrEnable = config.Contains("DTR");
            // TODO if necessary, make it configurable (PR are welcome)
            serial.Handshake = Handshake.None;

            return serial;
        }
    }
}
