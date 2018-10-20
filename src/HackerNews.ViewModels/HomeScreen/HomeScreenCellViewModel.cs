using System;
using System.Reactive;

namespace HackerNews.ViewModels.HomeScreen
{
    public class HomeScreenCellViewModel //: ReactiveObject
    {
        public IObservable<Unit> PageToPush { get; set; }

        public string Name { get; set; }
    }
}
