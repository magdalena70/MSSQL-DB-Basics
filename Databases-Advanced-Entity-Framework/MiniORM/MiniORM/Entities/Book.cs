using MiniORM.Attributes;
using System;

namespace MiniORM.Entities
{
    [Entity(TableName = "Books")]
    class Book
    {
        [Id]
        private int id;

        [Column(ColumnName = "Title")]
        private string title;

        [Column(ColumnName = "Author")]
        private string author;
        
        [Column(ColumnName = "PublishedOn")]
        private DateTime publishedOn;

        [Column(ColumnName = "Language")]
        private string language;

        [Column(ColumnName = "isHardCovered")]
        private bool isHardCovered;

        [Column(ColumnName = "Rating")]       
        private decimal rating;

        public Book(string title, string author, DateTime publishedOn, string language, bool isHardCovered, decimal rating)
        {
            this.Title = title;
            this.Author = author;
            this.PublishedOn = publishedOn;
            this.Language = language;
            this.IsHardCovered = isHardCovered;
            this.Rating = rating;
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }

        public string Author
        {
            get
            {
                return author;
            }

            set
            {
                author = value;
            }
        }

        public DateTime PublishedOn
        {
            get
            {
                return publishedOn;
            }

            set
            {
                publishedOn = value;
            }
        }

        public string Language
        {
            get
            {
                return language;
            }

            set
            {
                language = value;
            }
        }

        public bool IsHardCovered
        {
            get
            {
                return isHardCovered;
            }

            set
            {
                isHardCovered = value;
            }
        }

        public decimal Rating
        {
            get
            {
                return rating;
            }

            set
            {
                if (value < 0 || value > 10)
                {
                    throw new ArgumentOutOfRangeException("Rating cannot be > 10 and < 0.");
                }
                rating = value;
            }
        }
    }
}
