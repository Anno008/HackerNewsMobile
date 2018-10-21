using System.Reactive.Disposables;
using HackerNews.ViewModels.Posts;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;

namespace HackerNews.Views.Posts
{
    public partial class PostCellView : ReactiveViewCell<PostCellViewModel>
    {
        public PostCellView()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, true);

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, vm => vm.Title, view => view.TitleLabel.Text).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.PostDate, view => view.DateTimePostedLabel.Text).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.Author, view => view.AuthorNameLabel.Text).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.Text, view => view.TextLabel.Text).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.Text, view => view.TextLabel.IsVisible, vmText => !string.IsNullOrWhiteSpace(vmText)).DisposeWith(disposables);
            });
        }
    }
}