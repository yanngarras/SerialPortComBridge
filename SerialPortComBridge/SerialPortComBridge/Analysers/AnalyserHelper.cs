using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortComBridge.Analysers
{
    class AnalyserHelper
    {
        /// <summary>
        /// Returns analyser from its name
        /// </summary>
        /// <param name="analyserName"></param>
        /// <returns></returns>
        public static IAnalyser GetAnalyser(string analyserName)
        {
            var type = typeof(IAnalyser);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));
            if (types.Any(a => a.Name.ToUpper() == analyserName.ToUpper()))
            {
                Type t = types.First(a => a.Name.ToUpper() == analyserName.ToUpper());
                return (IAnalyser)Activator.CreateInstance(t);
            }
            Console.WriteLine("Unable to find analyser [" + analyserName + "]");
            return null;
        }
    }
}
