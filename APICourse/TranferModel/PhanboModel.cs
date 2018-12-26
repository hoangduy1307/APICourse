using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using APICourse.Models;

namespace APICourse.TranferModel
{
    public class PhanboModel
    {
        public int Idclass { get; set; }
        public int Idteacher { get; set; }
        [Required]
        [StringLength(20)]
        public string NameCourse { get; set; }
        [StringLength(100)]
        public string NameClass { get; set; }
        public string NameTeacher { get; set; }
        [Column(TypeName = "date")]
        public DateTime? StartDay { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FinishDay { get; set; }

        public int? Number { get; set; }
        public int? State { get; set; }
        public PhanboModel() { }
        public PhanboModel(Phanbo pb)
        {
            this.Idclass = pb.Idclass;
            this.Idteacher = pb.Idteacher;
        }

    }
}
