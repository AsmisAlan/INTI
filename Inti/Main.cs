using GeoUsers.Services;
using GeoUsers.Services.Logics;
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
    }
}
