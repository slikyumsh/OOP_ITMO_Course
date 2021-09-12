using System;
using Isu.Tools;

namespace Isu.Models
{
    public class Student
    {
        private string _name;
        private int _id;

        public Student()
        {
            _name = "Username";
            _id = Constants.MinId;
            Constants.MinId++;
        }

        public Student(string name)
        {
            _name = name;
            _id = Constants.MinId;
            Constants.MinId++;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public int Id
        {
            get
            {
                return _id;
            }
        }
    }
}