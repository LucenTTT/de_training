using System;
using System.Collections.Generic;

namespace de_training
{
    public partial class Book
    {
        public long BookId { get; set; }
        public string Title { get; set; } = null!;
        public long ReaderId { get; set; }

        public virtual Reader Reader { get; set; } = null!;
    }
}
