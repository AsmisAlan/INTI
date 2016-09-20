using GeoUsers.Services;
using GeoUsers.Services.Logics;
using GeoUsers.Services.Model;
using Inti.SmartSelectUtils;
using System.Windows.Forms;
using static GeoUsers.Services.SessionProvider;

namespace Inti
{
    public partial class Main : Form
    {
        readonly LocalidadLogic localidadLogic;

        private ContextSessionBlock ContextSessionBlock { get; set; }

        public Main(LocalidadLogic localidadLogic)
        {
            ContextSessionBlock = GeoUsersServices.SessionProvider.GetSessionContextBlock();

            this.localidadLogic = localidadLogic;

            InitializeComponent();
        }

        private bool LocalidadChangedCallback(IdAndValue localidad)
        {
            return true;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var smartSelectForm = new SmartSelectForm(localidadLogic.GetForSelection,
                                                      LocalidadChangedCallback,
                                                      localidadLogic.GetForSelection());

            smartSelectForm.ShowDialog();
        }
    }
}
