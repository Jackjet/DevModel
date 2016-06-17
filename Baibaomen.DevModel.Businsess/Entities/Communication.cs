using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baibaomen.DevModel.Businsess.Entities
{
    public class Communication:BaseEntity
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public int Creator { get; set; }
        
        public int PropertyId { get; set; }

        public virtual Property Property { get; set; }

    }
}
