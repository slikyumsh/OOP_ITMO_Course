using System;
using Isu.Tools;

namespace Isu.Models
{
    public class Student
    {
        private static int _minStudentId = 100000;
        private string _name;
        private int _id;

        public Student(string name)
        {
            _name = name;
            _id = _minStudentId;
            _minStudentId++;
        }

        public string Name => _name;

        public int Id => _id;
    }
}