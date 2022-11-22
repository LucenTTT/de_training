namespace Domain
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<ToDo> ToDoList { get; set; }
    }
}