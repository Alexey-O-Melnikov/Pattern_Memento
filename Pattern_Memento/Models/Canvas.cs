using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Memento.Models
{
    class Canvas
    {
        private List<Figure> _figures;
        public Canvas()
        {
            _figures = new List<Figure>(); 
        }
        public void AddCircle(int x, int y, int r, string color)
        {
            _figures.Add(new Circle(x, y, r, color));
        }
        public void AddRectangle(int x, int y, int width, int height, string color)
        {
            _figures.Add(new Rectangle(x, y, width, height, color));
        }
    }
}
