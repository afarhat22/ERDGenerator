using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERDGenerator
{
    internal class Relationship : IDiagramBox
    {
        private static int _nextId = 1;
        public int elementID { get; } = 2;

        public int ID { get; private set; }
        public string name { get; set; }
        public Entity entity1 { get; set; }
        public Entity entity2 { get; set; }
        public relationshipType e1Type { get; set; }
        public relationshipType e2Type { get; set; }

        

        public enum relationshipType
        {
            one, many
        }

        public Relationship(String n, Entity e1, Entity e2, int t1, int t2)
        {
            this.entity1 = e1;
            this.entity2 = e2;
            this.e1Type = (relationshipType)t1;
            this.e2Type = (relationshipType)t2;
            this.name = n;
            ID = _nextId++;
        }
    }
}
