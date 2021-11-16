using System;

namespace Banks
{
    public class Message
    {
        private string _text;

        public Message(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Empty string");
            _text = text;
        }

        public string Text => _text;
    }
}