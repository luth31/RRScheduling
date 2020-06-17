using System;

namespace RRScheduling {
    class Task {
        public Task(String Name, int StartTime, int BurstTime) {
            this.Name = Name;
            this.StartTime = StartTime;
            this.BurstTime = BurstTime;
        }
        public String Name;
        public int StartTime;
        public int BurstTime;
    }
}