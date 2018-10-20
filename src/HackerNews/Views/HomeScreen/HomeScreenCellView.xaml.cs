using System.Reactive.Disposables;
using HackerNews.ViewModels.HomeScreen;
using ReactiveUI;
using ReactiveUI.XamForms;

namespace HackerNews.Views.HomeScreen
{
    public partial class HomeScreenCellView : ReactiveViewCell<HomeScreenCellViewModel>
    {
		public HomeScreenCellView ()
		{
			InitializeComponent();

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel, vm => vm.Name, view => view.NameLabel.Text).DisposeWith(disposables);
            });
		}
	}
}