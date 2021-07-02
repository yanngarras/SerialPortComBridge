using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortComBridge.Analysers
{

    /// <summary>
    /// Interface that must implement every analyser
    /// </summary>
    interface IAnalyser
    {
        /// <summary>
        /// Fired when data arrives from First Serial Com Port
        /// </summary>
        /// <param name="byteArray">The received data</param>
        void onRequestData(byte[] byteArray);

        /// <summary>
        /// Fired when data arrives from Second Serial Com Port
        /// </summary>
        /// <param name="byteArray"></param>
        void onResponseData(byte[] byteArray);
    }
}
