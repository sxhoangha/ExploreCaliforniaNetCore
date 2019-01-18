using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExploreCalifornia.Models
{
    public class Post
    {
        public long Id { get; set; }
        private string _key;
        public string Key
        {
            get
            {
                if (_key==null)
                {
                    _key = Regex.Replace(Title.ToLower(), "[^a-z0-9]", "-");
                }
                return _key;
            }
            set
            {
                _key = value;
            }
        }
        [Required]
        [Display(Name ="Post Title")]
        [StringLength(100, MinimumLength =5,ErrorMessage ="Title must be between 5 and 100 characters long")]
        public string Title { get; set; }
        public string Author { get; set; }
        [DataType(DataType.MultilineText)]
        [Required]
        [StringLength(1000, MinimumLength =20, ErrorMessage = "Body must be between 20 and 1000 characters long")]
        public string Body { get; set; }
        public DateTime Posted { get; set; }

    }
}
