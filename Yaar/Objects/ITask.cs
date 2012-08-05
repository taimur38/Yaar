using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yaar.Objects
{
    interface ITask
    {
        DateTime Due { get; set; }
        string Title { get; set; }
        string Link { get; set; }
        string Description { get; set; }
        int Id { get; set; }
        Priority Priority { get; set; }
    }
}
