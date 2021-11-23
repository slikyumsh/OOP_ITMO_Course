using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Banks
{
    public class Parser
    {
        private readonly string _commandName;
        private readonly List<string> _arguments;

        public Parser(string input)
        {
            int spaceCounter = 0;
            _arguments = new List<string>();
            string result = string.Empty;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == ' ')
                {
                    if (spaceCounter == 0)
                        _commandName = result;
                    else
                        _arguments.Add(result);

                    spaceCounter++;
                    result = string.Empty;
                }
                else if (i == input.Length - 1)
                {
                    result += input[i];
                    _arguments.Add(result);
                }
                else
                {
                    result += input[i];
                }
            }
        }

        public string Command => _commandName;
        public IReadOnlyList<string> Arguments => _arguments;
    }
}