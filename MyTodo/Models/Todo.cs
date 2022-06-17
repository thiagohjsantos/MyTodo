using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyTodo.Models
{
    public class Todo
    {
        public int Id { get; set; }
        [DisplayName("Title")]
        [Required(ErrorMessage = "Required field")]
        public string Title { get; set; }  
        [DisplayName("Description")]
        public string Message { get; set; }
        [DisplayName("Done")]       
        public bool Done { get; set; }
        [DisplayName("Created at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [DisplayName("Last update at")] 
        public DateTime LastUpdateAt { get; set; } = DateTime.Now;
        [DisplayName("User")]
        public string User { get; set; }
        public string Image { get; set; }

    }
}
