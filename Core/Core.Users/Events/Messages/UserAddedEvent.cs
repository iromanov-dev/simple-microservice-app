using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Users.Events.Messages
{
    public class UserAddedEvent
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
