using GraphQLTodoList.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLTodoList.Features.Views
{
    public class UserView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; } = null;

        public List<TaskView> Tasks { get; set; } = new List<TaskView>();

        public UserView() { }

        public UserView(User user)
        {
            this.Id = user.Id;
            this.Name = user.Name;
            this.Password = user.Password;
            this.Email = user.Email;
            this.Age = user.Age;
            this.CreatedAt = user.CreatedAt;
            this.DeletedAt = user.DeletedAt;

            this.Tasks = user.Tasks.Select(t => new TaskView(t)).ToList();
        }
    }
}
