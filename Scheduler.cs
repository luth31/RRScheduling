using System;

namespace RRScheduling {
    class Scheduler {
        public Scheduler(int Quantum) {
            if (Quantum < 1)
                Quantum = 1;
            this.Quantum = Quantum;
        }

        int Quantum = 0;
    }
}