using System.Reactive.Disposables;
using HackerNews.ViewModels.HomeScreen;
using ReactiveUI;
using Xamarin.Forms;

namespace HackerNews.Views.HomeScreen
{
    public partial class HomeScreenView : ContentPageBase<HomeScreenViewModel>
    {
        public HomeScreenView()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, true);

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, vm => vm.Items, view => view.MenuItemsList.ItemsSource).DisposeWith(disposables);
                this.Bind(ViewModel, vm => vm.SelectedItem, view => view.MenuItemsList.SelectedItem).DisposeWith(disposables);
            });
        }
    }
}