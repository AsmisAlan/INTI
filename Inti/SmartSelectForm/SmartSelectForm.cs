using GeoUsers.Services.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inti.SmartSelectUtils
{
    public partial class SmartSelectForm : Form
    {
        public SmartSelectForm(Func<IEnumerable<IdAndValue>> dataFunction,
                               Func<IdAndValue, bool> itemChangedCallback,
                               IEnumerable<IdAndValue> selection)
        {
            InitializeComponent();

            smartSelect.Initialize(dataFunction,
                                    itemChangedCallback,
                                    selection);
        }
    }
}
