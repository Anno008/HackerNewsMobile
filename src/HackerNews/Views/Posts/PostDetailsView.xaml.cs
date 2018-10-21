using System.Reactive.Disposables;
using HackerNews.ViewModels.Posts;
using ReactiveUI;
using Xamarin.Forms;

namespace HackerNews.Views.Posts
{
    public partial class PostDetailsView : ContentPageBase<PostDetailsViewModel>
    {
        public PostDetailsView()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, true);

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, vm => vm.Comments, view => view.PostsList.ItemsSource).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.GetComments, view => view.PostsList.LoadMoreCommand);
                this.OneWayBind(ViewModel, vm => vm.CommentsAreLoading, view => view.ActivityIndicator.IsLoading).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.Post.Title, view => view.TitleLabel.Text).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.Post.By, view => view.AuthorLabel.Text).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.Post.Time, view => view.TimePostedLabel.Text).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.Post.Score, view => view.ScoreLabel.Text).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.Post.Kids.Count, view => view.CommentsLabel.Text).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.Post.Text, view => view.TextLabel.Text).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.Post.Text, view => view.TextLabel.IsVisible, vmText => !string.IsNullOrWhiteSpace(vmText)).DisposeWith(disposables);
            });
        }
    }
}