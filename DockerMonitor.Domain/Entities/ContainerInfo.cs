using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockerMonitor.Domain.Entities;

public class ContainerInfo
{
    public string Id { get; set; }
    public IList<string> Names { get; set; }
    public string Image { get; set; }
    public string State { get; set; }
    public DateTime Created { get; set; }
    public IDictionary<string, string> Labels { get; set; }
    public IEnumerable<string> Volumes { get; set; }
}
