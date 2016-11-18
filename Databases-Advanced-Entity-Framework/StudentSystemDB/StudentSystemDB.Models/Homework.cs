using System;
using System.ComponentModel.DataAnnotations;

namespace StudentSystemDB.Models
{
    public enum ContentTypes
    {
        Application, Pdf, Zip
    }

    public class Homework
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(1000)]
        public string Content { get; set; }

        [Required]
        public ContentTypes ContentType { get; set; }

        [Required]
        public DateTime SubmissionDate { get; set; }

        public Course Course { get; set; }

        public Student Student { get; set; }
    }
}
