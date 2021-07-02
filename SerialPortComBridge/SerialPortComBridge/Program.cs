using SerialPortComBridge.Analysers;
using System;
using System.IO.Ports;
using System.Text;

namespace SerialPortComBridge
{
    /// <summary>
    /// Create a bridge between 2 Serial Com Port 
    /// Usage : SerialPortComBridge.exe COMX-BAUD-SIZE-PARITY-STOP-[DTR]-[RTS] COMY-BAUD-SIZE-PARITY-STOP-[DTR]-[RTS]
    /// Ex : SerialPortComBridge.exe COM1-9600-8-N-2 COM2-115200-7-O-1-DTR-RTS
    /// </summary>
    class Program
    {
        // First Serial Com Port
        static SerialPort ComPortFirst;
        // Tag for first COM data in logs 
        static string TAG_FIST_COM_PORT = "";

        // Second Serial Com Port
        static SerialPort ComPortSecond;
        // Tag for second COM data in logs 
        static string TAG_SECOND_COM_PORT = "";

        static IAnalyser Analyser = null;

        /// <summary>
        /// Handler fired when data arrives on first Serial Com Port
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void firstPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            portDataReceived(sender, e, ComPortFirst, ComPortSecond, TAG_FIST_COM_PORT);
        }


        /// <summary>
        /// Handler fired when data arrives on second Serial Com Port
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void secondPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            portDataReceived(sender, e, ComPortSecond, ComPortFirst, TAG_SECOND_COM_PORT);
        }


        /// <summary>
        /// Fired when data arrives on a Serial Com Port. Aims to write on ComPortTarget data readen from ComPort
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="ComPort">Source Serial Com Port</param>
        /// <param name="ComPortTarget">Target Com Port</param>
        /// <param name="tag">Tag for logging purpose</param>
        private static void portDataReceived(object sender, SerialDataReceivedEventArgs e, SerialPort ComPort, SerialPort ComPortTarget, string tag)
        {
            // Read data from ComPort
            int i = 0;
            int bytesToRead = ComPort.BytesToRead;
            if (bytesToRead == 0) return;
            byte[] byteArray = new byte[bytesToRead];

            while (i < bytesToRead)
            {
                int b = ComPort.ReadByte();
                byteArray[i] = Convert.ToByte(b);
                i++;
            }

            // Write data to ComPortTarget
            try
            {
                ComPortTarget.Write(byteArray, 0, bytesToRead);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Log data to console if not analyser
            if (Analyser == null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(tag + " : ");
                foreach (var b in byteArray)
                {
                    sb.Append(b);
                    sb.Append("\t");
                }
                Console.WriteLine(sb.ToString());
            }
            else
            {
                if (ComPort == ComPortFirst)
                {
                    Analyser.onRequestData(byteArray);
                }
                else
                {
                    Analyser.onResponseData(byteArray);
                }
            }
        }

        /// <summary>
        /// DIplay usage to user
        /// </summary>
        static void ShowUsage()
        {
            Console.WriteLine("Usage : SerialPortComBridge.exe COMX-BAUD-SIZE-PARITY-STOP-[DTR]-[RTS] COMY-BAUD-SIZE-PARITY-STOP-[DTR]-[RTS] [Analyser]");
            Console.WriteLine("Ex : SerialPortComBridge.exe COM1-9600-8-N-2 COM2-115200-7-O-1-DTR-RTS Modbus");
            // TODO display available analysers 
        }

        /// <summary>
        /// Entry point, will open Serial Com Ports according to given parameters and start bridge
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Check arguments 
            if (args.Length < 2)
            {
                ShowUsage();
                return;
            }

            // Initialise analyser if any
            if (args.Length == 3)
            {
                Analyser = AnalyserHelper.GetAnalyser(args[2]);
            }

            // Try to open the both com ports
            try
            {
                ComPortFirst = ComPortConfig.ConfigureSerialPort(args[0]);
                TAG_FIST_COM_PORT = ComPortFirst.PortName;
                ComPortSecond = ComPortConfig.ConfigureSerialPort(args[1]);
                TAG_SECOND_COM_PORT = ComPortSecond.PortName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            // Check if ports are configured 
            if (ComPortFirst == null || ComPortSecond == null)
            {
                ShowUsage();
                return;
            }

            // Assign handler and open ports
            ComPortFirst.DataReceived +=
                          new System.IO.Ports.SerialDataReceivedEventHandler(firstPortDataReceived);
            ComPortFirst.Open();

            ComPortSecond.DataReceived +=
                new System.IO.Ports.SerialDataReceivedEventHandler(secondPortDataReceived);
            ComPortSecond.Open();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
