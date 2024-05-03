using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    //wird derzeit im coder nicht verwendet
    internal class ConfigInfo
    {
        public string ProjectName { get; set; } = string.Empty;

        public string Version { get; set; } = string.Empty;

        public Guid UUID { get; set; } = Guid.Empty;
    }
}
