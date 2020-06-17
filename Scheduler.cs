using System;
using System.Collections.Generic;

namespace RRScheduling {
    class Scheduler {
        public Scheduler(int Quantum) {
            if (Quantum < 1)
                Quantum = 1;
            this.Quantum = Quantum;
            taskList = new List<Task>();
        }
        public void AddTask(Task task) {
            // Keep list ordered by StartTime and by insertion order
            for (int i = taskList.Count; i > 0; --i) {
                if (task.StartTime >= taskList[i-1].StartTime) {
                    taskList.Insert(i, task);
                    return;
                }
            }
            taskList.Insert(0, task);
        }
        int Quantum = 0;
        List<Task> taskList;
    }
}