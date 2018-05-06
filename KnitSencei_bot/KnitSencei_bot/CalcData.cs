using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnitSencei_bot
{
   public class CalcData
    {
        public string KOmodel { get; set; }
        public string Model { get; set; }

        public string KOsize { get; set; }
        public string Size { get; set; }

        public string KOyarn { get; set; }
        public string Yarn { get; set; }
        

        public void PieceData(string lin)
        {
            string[] parts = lin.Split(';'); 
            KOmodel = parts[0];
            Model = parts[1];
            KOsize = parts[2];
            Size = parts[3];
            KOyarn = parts[4];
            Yarn = parts[5];
            
        }

        public List<CalcData> ReadFile(string filename)
        {
            
            List<CalcData> result = new List<CalcData>();
            using (StreamReader sdata = new StreamReader(filename))
            {
                string lin;
                while ((lin = sdata.ReadLine()) != null)
                {
                    CalcData calcul = new CalcData();
                    calcul.PieceData(lin);
                    result.Add(calcul);
                }
            }
            return result;


        }
    }
}
