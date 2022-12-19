using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace project_in_wpf2
{
    internal class serielepoort
    {
        public SerialPort poort;
        public serielepoort(string Poortnummer, int Baudrate)
        {
            if (poort == null)
            {
                poort = new SerialPort(Poortnummer, Baudrate);
            }
            poort.Open();
        }
        public void showlcd(string text)
        {
            if (poort != null && poort.IsOpen)
            {
                poort.WriteLine(text);
            }
        }
    }
}
