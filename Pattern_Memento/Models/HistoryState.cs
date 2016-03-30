using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pattern_Memento.Models
{
    class HistoryState
    {
        private int _indexState = 0;
        private List<Canvas> _canvases;
        public HistoryState()
        {
            _canvases = new List<Canvas>();
        }

        public void SaveState(Canvas canvas)
        {
            if(_indexState < _canvases.Count)
                _canvases.RemoveRange(_indexState, _canvases.Count - _indexState);
            _canvases.Add(canvas);
            _indexState++;
        }

        public Canvas LoadState(string go)
        {
            if (go == "cancel" && _indexState > 1)
                _indexState--;
            if (go == "restore" && _indexState < _canvases.Count)
                _indexState++;

            return _canvases[_indexState-1];
        }
    }
}
