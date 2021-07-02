using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortComBridge.Analysers
{
    /// <summary>
    /// Analyser for Modbus RTU protocol
    /// </summary>
    class Modbus : IAnalyser
    {
        enum ModbusFunction
        {
            READ_COIL = 1,
            READ_DISCRETE = 2,
            READ_HOLDING_REGISTER = 3,
            READ_INPUT_REGISTER = 4,
            WRITE_SINGLE_COILS = 5,
            WRITE_MULITPLE_COILS = 15,
            WRITE_SINGLE_REGISTER = 6,
            WRITE_MULTIPLE_REGISTERS = 16,
            READ_WRITE_MULTIPLE_REGISTERS = 23,
            MASK_WRITE_REGISTER = 22,
            READ_FIFO_QUEUE = 24,
            Read_File_record = 20,
            READ_FILE_RECORD = 20,
            WRITE_FILE_RECORD = 21,
            READ_EXCEPTION_STATUS = 7,
            GET_COM_EVENT_COUNTER = 11,
            GET_COM_EVENT_LOG = 12,
            REPORT_SLAVE_ID = 17,
            READ_DEVICE_IDENTIFICATION = 43,
            DIAGNOSTIC = 8,
            TIMEOUT = 131,
        }

        /// <summary>
        /// Constructor just diplaying info to say it's here
        /// </summary>
        public Modbus()
        {
            Console.WriteLine("Modbus analyser initialization...");
        }

        /// <summary>
        /// Log slave, function start, length
        /// </summary>
        /// <param name="byteArray"></param>
        public void onRequestData(byte[] byteArray)
        {
            if (byteArray == null)
            {
                Console.WriteLine("Received null data");
                return;
            }
            StringBuilder sb = new StringBuilder();

            try
            {
                ModbusFunction func = (ModbusFunction)Enum.Parse(typeof(ModbusFunction), byteArray[1].ToString());
                sb.Append("=> Slave : " + byteArray[0]);
                sb.Append("\t");
                sb.Append("Func : " + func);
                sb.Append("\t");
                sb.Append("Start : " + (byteArray[2] * 256 + byteArray[3]));
                sb.Append("\t");
                sb.Append("Length : " + (byteArray[4] * 256 + byteArray[5]));

                // TODO add and check checksum

                // Data detail
                sb.Append("(");
                foreach (var b in byteArray)
                {
                    sb.Append(b);
                    sb.Append(" ");
                }
                sb.Append(")");

            }
            catch (Exception ex)
            {
                sb.Append("Unknown function : " + byteArray[1].ToString() + " on slave " + byteArray[0]);
                sb.Append(ex.Message);
            }
            Console.WriteLine(sb.ToString());
        }

        public void onResponseData(byte[] byteArray)
        {
            if (byteArray == null)
            {
                Console.WriteLine("Received null data");
                return;
            }

            if (byteArray == null)
            {
                Console.WriteLine("Received null data");
                return;
            }
            StringBuilder sb = new StringBuilder();

            try
            {
                ModbusFunction func = (ModbusFunction)Enum.Parse(typeof(ModbusFunction), byteArray[1].ToString());
                sb.Append("<= Slave : " + byteArray[0]);
                sb.Append("\t");
                sb.Append("Func : " + func);
                sb.Append("\t");
                sb.Append("Length : " + byteArray[2]);
                sb.Append("\t");
                sb.Append("Bytes :");
                for (int i = 3; i < byteArray.Length - 2; i++)
                {
                    sb.Append(" ");
                    sb.Append(byteArray[i]);
                }

                // TODO add and check checksum

                // Data detail
                sb.Append("(");
                foreach (var b in byteArray)
                {
                    sb.Append(b);
                    sb.Append(" ");
                }
                sb.Append(")");

            }
            catch (Exception ex)
            {
                sb.Append("Unknown function : " + byteArray[1].ToString() + " on slave " + byteArray[0]);
                sb.Append(ex.Message);
            }
            Console.WriteLine(sb.ToString());
        }
    }
}
