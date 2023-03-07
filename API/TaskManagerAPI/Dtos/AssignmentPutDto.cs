namespace TaskManagerAPI.Dtos
{
    public class AssignmentPutDto
    {
        public Guid Id { get; set; }        
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreation { get; set; }
        public bool IsDone { get; set; }
        public DateTime Deadline { get; set; }
    }
}
