using System;
using HackerNews.Core.Models;
using ReactiveUI;

namespace HackerNews.ViewModels.Posts
{
    public class PostCellViewModel : ReactiveObject
    {
        private string _title;
        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        private DateTime _postDate;
        public DateTime PostDate
        {
            get => _postDate;
            set => this.RaiseAndSetIfChanged(ref _postDate, value);
        }

        private string _author;
        public string Author
        {
            get => _author;
            set => this.RaiseAndSetIfChanged(ref _author, value);
        }

        private string _text;
        public string Text
        {
            get => _text;
            set => this.RaiseAndSetIfChanged(ref _text, value);
        }

        public PostCellViewModel(Post post)
        {
            _title = post.Title;
            _postDate = post.Time;
            _author = post.By;
            _text = post.Text;
        }
    }
}
