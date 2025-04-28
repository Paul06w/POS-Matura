namespace _241104_KanbanBoard.Models
{
    public class KanbanTask
    {
        public int Id { get; set; }
        public string? Todo { get; set; }
        public string? Responsible { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? DoneDate { get; set; }
        public string? Status { get; set; }
        public string? UserName { get; set; }
    }
}
