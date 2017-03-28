using GeoUsers.Services;
using GeoUsers.Services.Utils;
using System;
using System.Threading.Tasks;

namespace GeoUsersUI.Utils
{
    public static class RequestService
    {
        public async static Task<bool> Execute(Action action)
        {
            var result = await Task.Run(() =>
            {
                using (var sessionContext = GeoUsersServices.SessionProvider.GetSessionContextBlock())
                {
                    try
                    {
                        action();

                        return true;
                    }
                    catch (Exception e)
                    {
                        Logger.Log(e);

                        sessionContext.ExceptionThrown = true;

                        MessageBoxUtils.Error(e.Message);

                        return false;
                    }
                }
            });

            return result;
        }
    }
}
