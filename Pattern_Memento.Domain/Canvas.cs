using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pattern_Memento.Domain
{
    public class Canvas
    {
        public int CanvasID { get; set; }
        public virtual List<Figure> Figures { get; set; }
    }
}
