using Xamarin.Forms;

namespace HackerNews.Controls
{
    public class CustomActivityIndicator : ActivityIndicator
    {
        public static readonly BindableProperty IsLoadingProperty =
            BindableProperty.Create(nameof(IsLoading), typeof(bool), typeof(CustomActivityIndicator), default(bool),
                propertyChanged: OnIsLoadingPropertyChanged);

        public bool IsLoading
        {
            get => (bool)GetValue(IsLoadingProperty);
            set => SetValue(IsLoadingProperty, value);
        }

        public CustomActivityIndicator()
        {
            this.HeightRequest = 10;
        }

        private static void OnIsLoadingPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as CustomActivityIndicator;
            if (control != null && newValue is bool value)
            {
                control.IsVisible = value;
                control.IsRunning = value;
            }
        }
    }
}