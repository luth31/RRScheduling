using System;

namespace RRScheduling {
    class Task {
        public Task(String Name, int StartTime, int BurstTime) {
            Console.WriteLine("Created {0}: {1}-{2}", Name, StartTime, BurstTime);
            this.Name = Name;
            this.StartTime = StartTime;
            this.BurstTime = BurstTime;
            ExecutedTime = 0;
        }
        public String Name;
        public int StartTime;
        public int BurstTime;
        public int ExecutedTime;
        public int remainingTime {
            get {
                return BurstTime - ExecutedTime;
            }
        }
    }
}