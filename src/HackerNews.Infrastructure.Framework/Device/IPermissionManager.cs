using System;
using Plugin.Permissions.Abstractions;

namespace HackerNews.Infrastructure.Framework.Device
{
    public interface IPermissionManager
    {
        IObservable<bool> RequestPermission(Permission permission);
    }
}
