using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoUsers.Services.Model
{
    public class IdAndValue
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}
