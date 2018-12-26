using System;
using System.Collections.Generic;

namespace APICourse.Models
{
    public partial class Phanbo
    {
        public int Idclass { get; set; }
        public int Idteacher { get; set; }

        public virtual Class IdclassNavigation { get; set; }
    }
}
