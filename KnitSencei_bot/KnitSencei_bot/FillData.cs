using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnitSencei_bot
{
    public class FillData
    {
        public void Fill_Model(List<DataOfdata> dd)
        {
            string path = "model.txt";
            int number = 0;

            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    DataOfdata data = new DataOfdata();
                    data.num = number;
                    data.kind = line;
                    dd.Add(data);
                    number++;
                }
            }
        }

        public void Fill_Size(List<DataOfdata> dd)
        {
            string path = "size.txt";
            int number = 0;

            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    DataOfdata data = new DataOfdata();
                    data.num = number;
                    data.kind = line;
                    dd.Add(data);
                    number++;
                }
            }
        }

        public void Fill_Yarn(List<DataOfdata> dd)
        {
            string path = "yarn.txt";
            int number = 0;

            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    DataOfdata data = new DataOfdata();
                    data.num = number;
                    data.kind = line;
                    dd.Add(data);
                    number++;
                }
            }
        }
    }
}
