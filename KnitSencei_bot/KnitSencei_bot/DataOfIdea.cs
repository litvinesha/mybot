using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnitSencei_bot
{
    public class DataOfIdea
    {
        public string ID { get; set; }
        public string Product { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }

        public void Piece_idea(string line)
        {
            string[] parts = line.Split(';');  //Разделитель в CVS файле.
            ID = parts[0];
            Product = parts[1];
            Name = parts[2];
            Description = parts[3];
            Photo = parts[4];
        }

        public List<DataOfIdea> ReadFile(string filename)
        {
            List<DataOfIdea> res = new List<DataOfIdea>();
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    DataOfIdea idea = new DataOfIdea();
                    idea.Piece_idea(line);
                    res.Add(idea);
                }
            }
            return res;
        }
    }
}
