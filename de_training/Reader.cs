using System;
using System.Collections.Generic;

namespace de_training
{
    public partial class Reader
    {
        public Reader()
        {
            Books = new HashSet<Book>();
        }

        public long ReaderId { get; set; }
        public string Pib { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; }
    }
}
