using System;

namespace IsuExtra.Models
{
    public class Professor
    {
        private string _name;
        private Guid _id;

        public Professor()
        {
            _name = "ProfessorUserName";
            _id = Guid.NewGuid();
        }

        public Professor(string name)
        {
            _name = name;
            _id = Guid.NewGuid();
        }
    }
}