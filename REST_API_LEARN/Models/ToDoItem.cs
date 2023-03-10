using System.ComponentModel.DataAnnotations;

namespace REST_API_LEARN.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
        public string? Secret { get; set; }
    }

    public class TodoItemDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}