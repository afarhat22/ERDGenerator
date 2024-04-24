using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERDGenerator
{
    internal class Entity
    {
        private static int _nextId = 1;

        public int ID { get; private set; }
        public String name {  get; set; }
        public List<Attribute> Attributes { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public Entity(String n)
        {
            ID = _nextId++;
            Attributes = new List<Attribute>();
            name = n;
        }

        public void AddAttribute(Attribute attribute)
        {
            Attributes.Add(attribute);
        }

    }
}
