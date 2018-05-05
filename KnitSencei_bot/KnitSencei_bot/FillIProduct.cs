using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnitSencei_bot
{
    public class FillIProduct
    {
        public void Fill_product (List<DataOfProduct> idea)
        {
            string path = "idea.txt";
            int number = 0;

            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    DataOfProduct data = new DataOfProduct();
                    data.number = number;
                    data.product = line;
                    idea.Add(data);
                    number++;
                }
            }
        }

        public void Read_csv ()
        {
            
        }
    }
}
