using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqLabs.作業
{
    internal class Students
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public int Chi { get; set; }
        public int Eng { get; set; }
        public int Math { get; set; }
        public string Gender { get; set; }


    }

    internal class Student
    {
        public string Name { get; set; }
        public int Chi { get; set; }
        public int Eng { get; set; }
        public int Math { get; set; }
        public string Gender { get; set; }

        public int Sum { get { return Chi + Eng + Math; } }
        public int Avg { get { return ((Chi + Eng + Math) / 3); } }
        

        
    }
}
   
