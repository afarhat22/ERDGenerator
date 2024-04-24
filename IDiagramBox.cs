using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERDGenerator
{
    internal interface IDiagramBox
    {
        string name { get; }
        int elementID { get; }
    }
}
