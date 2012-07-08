using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yaar.Objects
{
    class ScheduledTask
    {
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        
        public ScheduledTask(DateTime dateTime, string description)
        {
            DateTime = dateTime;
            Description = description;
        }

        public override bool Equals(object obj)
        {
            return obj is ScheduledTask && obj.GetHashCode() == this.GetHashCode();
        }

        public override int GetHashCode()
        {
            return (this.DateTime + this.Description).GetHashCode();
        }
    }
}
