using System;
using System.IO;
using HackerNews.Core.Models;
using HackerNews.ViewModels.Helpers;
using HtmlAgilityPack;
using ReactiveUI;

namespace HackerNews.ViewModels.Posts
{
    public class PostCellViewModel : ReactiveObject
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }

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

        private int _commentsCount;
        public int CommentsCount
        {
            get => _commentsCount;
            set => this.RaiseAndSetIfChanged(ref _commentsCount, value);
        }

        private int _score;
        public int Score
        {
            get => _score;
            set => this.RaiseAndSetIfChanged(ref _score, value);
        }

        private string _url;
        public string Url
        {
            get => _url;
            set => this.RaiseAndSetIfChanged(ref _url, value);
        }

        public PostCellViewModel(Post post)
        {
            _id = post.Id;
            _title = post.Title;
            _postDate = post.Time;
            _author = post.By;
            _text = post.Text.ConvertHtml();
            _commentsCount = post.Kids?.Count ?? 0;
            _score = post.Score;
            _url = post.Url;
        }
    }
}
