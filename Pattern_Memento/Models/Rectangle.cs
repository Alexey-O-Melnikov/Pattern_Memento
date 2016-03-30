using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Memento.Models
{
    class Rectangle : Figure
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Rectangle(int x, int y, int width, int height, string color)
        {
            StartPoint = new Point(x, y);
            Width = width;
            Height = height;
            Color = color;
        }
    }
}
