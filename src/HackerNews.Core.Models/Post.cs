﻿using System;
using System.Collections.Generic;

namespace HackerNews.Core.Models
{
    public class Post
    {
        /// <summary>
        /// The item's unique id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The user name of the post's author.
        /// </summary>
        public string By { get; set; }

        /// <summary>
        /// The comment, story or poll text. HTML!!!><
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The title of the story, poll or job.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// In the case of stories or polls, the total comment count.
        /// </summary>
        public int Descendants { get; set; }

        /// <summary>
        /// The ids of the item's comments
        /// </summary>
        public List<int> Kids { get; set; }

        /// <summary>
        /// The story's score
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Creation of the item
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// The type of the post/story
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The URL of the story.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Signals if the item has been deleted
        /// </summary>
        public bool Deleted { get; set; }
    }
}
