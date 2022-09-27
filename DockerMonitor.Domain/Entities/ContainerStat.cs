using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerMonitor.Domain.Entities
{
    public class ContainerStat
    {

        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public ulong CPUUsage { get; set; }
        public uint Cores { get; set; }
        public ulong MemoryUsage { get; set; }
        public ulong MemoryMax { get; set; }
        public ulong ReadSize { get; set; }
        public ulong WriteSize { get; set; }

        public string DBContainerId { get; set; }
        public DBContainer DBContainer { get; set; }
    }
}
