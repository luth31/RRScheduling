using System;
using System.Collections.Generic;

namespace RRScheduling {
    class Scheduler {
        public Scheduler(int Quantum, bool DebugMessages = false, bool IdleMode = true) {
            if (Quantum < 1)
                Quantum = 1;
            this.Quantum = Quantum;
            taskList = new List<Task>();
            Timeline = new Timeline();
            idleTask = new Task("Idle", 0, 0);
        }
        public void AddTask(Task task) {
            // Keep list ordered by StartTime and by insertion order
            for (int i = taskList.Count; i > 0; --i) {
                if (task.StartTime >= taskList[i-1].StartTime) {
                    taskList.Insert(i, task);
                    return;
                }
            }
            // If the iteration doesn't insert the element, add it here because it's the lowest
            taskList.Insert(0, task);
        }
        public void Begin() {
            Console.WriteLine("Quantum: {0}", Quantum);
            Queue<Task> taskQueue = new Queue<Task>();
            while (taskQueue.Count > 0 || taskList.Count > 0 || nextTask != null) {

                // If taskList is not empty
                if (taskList.Count > 0) {
                    // Iterate through all tasks in taskList
                    foreach (var task in new List<Task>(taskList))
                        // Enqueue all due tasks and remove them from taskList
                        if (task.StartTime <= ExecutionTime) {
                            Console.WriteLine("Enqueued {0} at {1} due {2}", task.Name, ExecutionTime, task.StartTime);
                            taskQueue.Enqueue(task);
                            taskList.Remove(task);
                        }
                }
                if (nextTask != null) {
                    taskQueue.Enqueue(nextTask);
                    nextTask = null;
                }
                // There's nothing to execute so far, "sleep" until next task is due
                if (taskQueue.Count == 0) {
                    var task = taskList[0];
                    Console.WriteLine("Sleep: {0}-{1}", ExecutionTime, task.StartTime);
                    Timeline.AddSlice(idleTask, ExecutionTime, task.StartTime);
                    ExecutionTime = task.StartTime;
                    continue;
                }
                var t = taskQueue.Dequeue();
                int execTime = Math.Min(t.remainingTime, Quantum);
                Console.WriteLine("Exec {0}: {1}-{2} (remaining: {3})", t.Name, ExecutionTime, ExecutionTime+execTime, t.remainingTime);
                Timeline.AddSlice(t, ExecutionTime, ExecutionTime+execTime);
                t.ExecutedTime += execTime;
                ExecutionTime += execTime;
                if (t.remainingTime > 0)
                    nextTask = t;
            }
            foreach (var slice in Timeline.History) {
                Console.WriteLine("{0}: {1}-{2}", slice.Item1.Name, slice.Item2, slice.Item3);
            }
        }
        Task nextTask = null;
        Task idleTask;
        int Quantum = 0;
        int ExecutionTime = 0;
        List<Task> taskList;
        Timeline Timeline;
        bool idleMode;
        bool debugMessages;
    }
}