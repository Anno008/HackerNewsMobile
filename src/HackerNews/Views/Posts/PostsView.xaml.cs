using System.Reactive.Disposables;
using System.Reactive.Linq;
using HackerNews.ViewModels.Posts;
using ReactiveUI;
using Xamarin.Forms;

namespace HackerNews.Views.Posts
{
    public partial class PostsView : ContentPageBase<PostsViewModel>
    {
        public PostsView()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, true);

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, vm => vm.Posts, view => view.PostsList.ItemsSource).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.GetPosts, view => view.PostsList.LoadMoreCommand);
                this.OneWayBind(ViewModel, vm => vm.IsLoading, view => view.ActivityIndicator.IsLoading).DisposeWith(disposables);
                Observable.Return(0).InvokeCommand(ViewModel, x => x.GetPosts);
            });
        }
    }
}