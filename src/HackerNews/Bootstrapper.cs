using HackerNews.Core;
using HackerNews.Core.Rest.RestApiServices;
using HackerNews.Core.RestRepository.Rest;
using HackerNews.Infrastructure.Framework.Device;
using HackerNews.Infrastructure.Framework.Globalization;
using HackerNews.Infrastructure.Framework.Runtime;
using HackerNews.Infrastructure.Framework.Scheduler;
using HackerNews.ViewModels.HomeScreen;
using HackerNews.ViewModels.Posts;
using HackerNews.Views.HomeScreen;
using HackerNews.Views.Posts;
using Sextant;
using Splat;

namespace HackerNews
{
    public class Bootstrapper
    {
        internal void RegisterServices()
        {
            Texts.Culture = RuntimeContext.Current.Culture;

            // Infrastructure Services
            Locator.CurrentMutable.Register(() => new SchedulerService(), typeof(ISchedulerService));
            Locator.CurrentMutable.Register(() => new Connectivity(), typeof(IConnectivity));
            Locator.CurrentMutable.Register(() => new PermissionManager(), typeof(IPermissionManager));

            // Core services
            Locator.CurrentMutable.Register(() => new HackerNewsService(), typeof(IHackerNewsService));

            // Rest services
            Locator.CurrentMutable.Register(() => new HackerNewsRestService(), typeof(IHackerNewsRestService));
            Locator.CurrentMutable.Register(() => new ApiService(), typeof(IApiService));
        }

        internal void RegisterViews()
        {
            SextantHelper.RegisterView<HomeScreenView, HomeScreenViewModel>();
            SextantHelper.RegisterView<PostsView, PostsViewModel>();
            SextantHelper.RegisterView<PostDetailsView, PostDetailsViewModel>();
        }
    }
}
