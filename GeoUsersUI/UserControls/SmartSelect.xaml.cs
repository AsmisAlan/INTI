using GeoUsers.Services.Model;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Models.ViewModels.SmartSelect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GeoUsersUI.UserControls
{
    /// <summary>
    /// Interaction logic for SmartSelect.xaml
    /// </summary>
    public partial class SmartSelect : UserControl
    {
        public SmartSelectViewModel ViewModel { get; set; }

        public SmartSelect()
        {
            InitializeComponent();
        }

        public void Initialize(Func<IEnumerable<IdAndValue>> dataFunction,
                               IEnumerable<IdAndValue> selection)
        {
            ViewModel.Initialize(dataFunction, selection);
        }

        public void RemoveEntityButton_Click(object sender, EventArgs e)
        {

        }

        public void AddEntityButton_Click(object sender, EventArgs e)
        {

        }
    }
}
