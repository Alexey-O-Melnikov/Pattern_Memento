using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Memento.Domain
{
    abstract public class Figure
    {
        public int FigureID { get; set; }
        //[ForeignKey("Canvas")]
        public int CanvasID { get; set; }
        public string Color { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
