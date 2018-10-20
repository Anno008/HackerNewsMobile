using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Acr.UserDialogs;
using HackerNews.Infrastructure.Framework.Globalization;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace HackerNews.Infrastructure.Framework.Device
{
    public class PermissionManager : IPermissionManager
    {
        public IObservable<bool> RequestPermission(Permission permission)
        {
            return CrossPermissions
                        .Current
                        .CheckPermissionStatusAsync(permission)
                        .ToObservable()
                        .SelectMany(status =>
                        {
                            if (status == PermissionStatus.Granted)
                            {
                                return Observable.Return(true);
                            }
                            else
                            {
                                return CrossPermissions
                                    .Current
                                    .ShouldShowRequestPermissionRationaleAsync(permission)
                                    .ToObservable()
                                    .SelectMany(shouldShowDialog =>
                                    {
                                        var promptedResult = Observable.Return(Unit.Default);
                                        if (shouldShowDialog)
                                        {
                                            promptedResult = UserDialogs
                                                .Instance
                                                .AlertAsync(string.Format(Texts.PermissionPromptFormat, permission))
                                                .ToObservable();
                                        }
                                        return promptedResult;
                                    })
                                    .SelectMany(_ => CrossPermissions.Current.RequestPermissionsAsync(permission).ToObservable())
                                    .Select(results => results.ContainsKey(permission) && results[permission] == PermissionStatus.Granted);
                            }
                        });
        }
    }
}