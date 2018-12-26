using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICourse.Models;

namespace APICourse.TranferModel
{
    public class PhanboModel
    {
        public int Idclass { get; set; }
        public int Idteacher { get; set; }

        public PhanboModel() { }
        public PhanboModel(Phanbo pb)
        {
            this.Idclass = pb.Idclass;
            this.Idteacher = pb.Idteacher;
        }

    }
}
