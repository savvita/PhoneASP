using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneDB.Model
{
    public class PhoneModel
    {
        public int Id { get; set; }
        public string Producer { get; set; } = null!;
        public string Model { get; set; } = null!;
        public decimal Price { get; set; }

    }
}
