using System;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels
{
    public class BaseListViewModel
    {
        public Func<Task<bool>> DeleteFunction { get; set; }

        public Func<Task<bool>> EditFunction { get; set; }

        public Func<Task<bool>> CreateFunction { get; set; }

        public Func<bool> CloseFunction { get; set; }
    }
}
