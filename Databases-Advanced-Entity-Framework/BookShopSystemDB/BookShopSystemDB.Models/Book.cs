using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookShopSystemDB.Models
{
    public enum EditionTypes
    {
        Normal, Promo, Gold
    }

    public enum AgeRestrictions
    {
        Minor, Teen, Adult
    }

    public class Book
    {
        private ICollection<Category> categories;
        private ICollection<Book> relatedBooks;
        

        public Book()
        {
            this.categories = new HashSet<Category>();
            this.relatedBooks = new HashSet<Book>();
        }

        [Key]
        public int Id { get; set; }

        [Required, MinLength(1), MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public virtual EditionTypes EditionType { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Copies { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public virtual AgeRestrictions AgeRestriction { get; set; }

        public virtual Author Author { get; set; }

        public virtual ICollection<Category> Categories
        {
            get { return this.categories; }
            set { this.categories = value; }
        }

        public virtual ICollection<Book> RelatedBooks
        {
            get { return this.relatedBooks; }
            set { this.relatedBooks = value; }
        }
    }
}
