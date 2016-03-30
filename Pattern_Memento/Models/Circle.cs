using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Memento.Models
{
    class Circle : Figure
    {
        public int Radius { get; set; }
        public Circle(int x, int y, int r, string color)
        {
            StartPoint = new Point(x, y);
            Radius = r;
            Color = color;
        }
    }
}
