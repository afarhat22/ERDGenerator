using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERDGenerator
{
    internal class Attribute : IDiagramBox
    {
        private static int _nextId = 1;
        public int elementID { get; } = 3;

        public int ID { get; private set; }
        public string name { get; private set; }
        public bool isPrimaryKey { get; set; }

        public Attribute(String n)
        {
            ID = _nextId++;
            name = n;
        }

    }
}
