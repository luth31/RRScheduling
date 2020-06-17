using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RRScheduling {
    class Timeline {
        public Timeline() {
            _History = new List<(Task, int, int)>();
        }
        public void AddSlice(Task task, int Start, int End) {
            _History.Add((task, Start, End));
        }
        public ReadOnlyCollection<(Task, int, int)> History => _History.AsReadOnly();
        List<(Task, int, int)> _History;
    }
}