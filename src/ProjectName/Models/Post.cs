using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ProjectName.Models
{

    [Table("post")]
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int ID { get; set; }
        [Column("title", TypeName = "varchar(30)")]
        [Display(Name = "Title")]
        public string Title { get; set; } = "";
        [Column("content", TypeName = "varchar(30)")]
        [Display(Name = "Content")]
        public string Content { get; set; } = "";

        [Column("user_id")]
        public string? UserID { get; set; }

        [ForeignKey(nameof(UserID))]
        [ValidateNever]
        public virtual User User { get; set; }
    }
}