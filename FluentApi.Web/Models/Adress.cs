using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentApi.Web.Models
{
    public class Adress
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Province { get; set; }
        public string PostCode { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
