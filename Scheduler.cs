using System;
using System.Collections.Generic;

namespace RRScheduling {
    class Scheduler {
        public Scheduler(int Quantum) {
            if (Quantum < 1)
                Quantum = 1;
            this.Quantum = Quantum;
            taskList = new List<Task>();
            Timeline = new Timeline();
            Console.WriteLine("Creating tasks...");
            CConsole.Write(ConsoleColor.Green, "Name\tStart\tBurst\n");
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
            taskListCopy = new List<Task>(taskList);
            int idleTime = 0;
            CDebug.WriteLine(ConsoleColor.Yellow, "Debug information:");
            Debug.WriteLine("Quantum: {0}", Quantum);
            Queue<Task> taskQueue = new Queue<Task>();
            // Run this while as long as there are more than 0 elements in queue or list or nextTask isn't null (needed when there's only 1 element left)
            while (taskQueue.Count > 0 || taskList.Count > 0 || nextTask != null) {

                // If taskList is not empty
                if (taskList.Count > 0) {
                    // Iterate through all tasks in taskList
                    foreach (var task in new List<Task>(taskList))
                        // Enqueue all due tasks and remove them from taskList
                        if (task.StartTime <= ExecutionTime) {
                            Debug.WriteLine("Enqueued {0} at {1} due {2}", task.Name, ExecutionTime, task.StartTime);
                            taskQueue.Enqueue(task);
                            taskList.Remove(task);
                        }
                }
                // If there is a task to be added to the queue, add it then set nextTask = null (so the nextTask doesn't carry over when not intended)
                if (nextTask != null) {
                    taskQueue.Enqueue(nextTask);
                    nextTask = null;
                }
                // There's nothing to execute so far, "sleep" (maybe I should also do that) until next task is due
                if (taskQueue.Count == 0) {
                    var task = taskList[0];
                    Debug.WriteLine("Sleep: {0}-{1}", ExecutionTime, task.StartTime);
                    // Add slice for sleep time
                    Timeline.AddSlice(idleTask, ExecutionTime, task.StartTime);
                    // "Jump" ExecutionTime to task.StartTime to simulate a sleep and add sum their difference to idleTime
                    idleTime += task.StartTime - ExecutionTime;
                    ExecutionTime = task.StartTime;
                    continue;
                }
                // Dequeue task and have t point to the Task object (objects are passed by reference so it's merely a pointer-alike; garbage collector will not touch it as long as there is a reference to it)
                var t = taskQueue.Dequeue();
                // Get minimum between remaining execution time of Task and Quantum; easiest way to handle execution when remaining time < Quantum
                int execTime = Math.Min(t.remainingTime, Quantum);
                Debug.WriteLine("Exec {0}: {1}-{2} (remaining: {3})", t.Name, ExecutionTime, ExecutionTime+execTime, t.remainingTime);
                // Add the "slice" to the History of the Timeline; needed for computation of avg wait time and such
                Timeline.AddSlice(t, ExecutionTime, ExecutionTime+execTime);
                // Increase executed time of task and of Scheduler
                t.ExecutedTime += execTime;
                ExecutionTime += execTime;
                // If task isn't finished yet reference it using nextTask; this way it can be handled after enqueuing remaining tasks from taskList
                if (t.remainingTime > 0)
                    nextTask = t;
                // Set t's end time to current ExecutionTime (easier to cache here than searching the History) 
                else
                    t.EndTime = ExecutionTime;
            }
            Console.WriteLine("\nTimeline:");
            CConsole.WriteLine(ConsoleColor.Red, "Name\tFrom\tTo");
            foreach (var slice in Timeline.History) {
                Console.WriteLine("{0}:\t{1}\t{2}\t", slice.Item1.Name, slice.Item2, slice.Item3);
            }
            int sumWaitTime = 0;
            Console.WriteLine("\nTasks stats:");
            CConsole.WriteLine(ConsoleColor.Magenta, "Name\tCTime\tTATime\tWTime");
            foreach (var task in taskListCopy) {
                int turnAroundTime = (task.EndTime-task.StartTime);
                int waitTime = turnAroundTime - task.BurstTime;
                Console.WriteLine("{0}:\t{1}\t{2}\t{3}", task.Name, task.EndTime, turnAroundTime, waitTime);
                sumWaitTime += waitTime;
            }
            Console.WriteLine("\nOverall stats:");
            Console.WriteLine("Total execution time: {0}", ExecutionTime);
            Console.WriteLine("Average wait time: {0}", sumWaitTime/taskListCopy.Count);
            Console.WriteLine("Idle time: {0}", idleTime);
            Console.ReadKey();
        }
        Task nextTask = null;
        Task idleTask;
        int Quantum = 0;
        int ExecutionTime = 0;
        List<Task> taskList;
        List<Task> taskListCopy;
        Timeline Timeline;
    }
}