using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoUsers.Services.Model.DataTransfer
{
    public class IdAndValue : BaseNotifierEntity
    {
        private int id { get; set; }
        private string value { get; set; }

        public int Id
        {
            get { return id; }

            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Value
        {
            get { return value; }

            set
            {
                this.value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
