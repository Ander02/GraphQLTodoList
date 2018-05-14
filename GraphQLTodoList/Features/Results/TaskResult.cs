using GraphQLTodoList.Domain;
using System;

namespace GraphQLTodoList.Features.Results
{
    public class TaskResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public UserResult User { get; set; }

        public TaskResult() { }

        public TaskResult(Task task)
        {
            this.Id = task.Id;
            this.Name = task.Name;
            this.Description = task.Description;
            this.CreatedAt = task.CreatedAt;
            this.CompletedAt = task.CompletedAt;
            this.DeletedAt = task.CompletedAt;

            this.User = task.User != null ? new UserResult(task.User) : null;
        }
    }
}