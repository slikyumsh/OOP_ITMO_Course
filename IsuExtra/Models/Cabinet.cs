using System;

namespace IsuExtra.Models
{
    public class Cabinet
    {
        private Guid _id;

        public Cabinet()
        {
            _id = Guid.NewGuid();
        }

        public Guid Id() => _id;
    }
}