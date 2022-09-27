using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerMonitor.Domain.Entities
{
    public class DBContainer
    {

        public string DBContainerId { get; set; }

        public List<ContainerStat> Stats { get; } = new();

    }
}
