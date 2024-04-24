using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERDGenerator
{
    internal class AllDiagramBoxes
    {
        public List<Entity> entities {  get; set; }
        public List<Relationship> relationships { get; set; }
        public List<Attribute> attributes { get; set; }
    }
}
