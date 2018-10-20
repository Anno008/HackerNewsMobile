using Plugin.Connectivity;

namespace HackerNews.Infrastructure.Framework.Device
{
    public class Connectivity : IConnectivity
    {
        public bool IsConnected => CrossConnectivity.Current.IsConnected;
    }
}
