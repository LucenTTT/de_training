using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ToDo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public User User { get; set; }

    }
}
