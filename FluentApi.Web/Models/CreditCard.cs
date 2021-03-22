using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentApi.Web.Models
{
    public class CreditCard
    {
        public int Id { get; set; }
        public string number { get; set; }
        public DateTime ValidDate { get; set; }
    }
}
