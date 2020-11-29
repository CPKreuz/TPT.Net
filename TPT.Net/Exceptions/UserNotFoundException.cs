using System;
using System.Collections.Generic;
using System.Text;

namespace TPT.Exceptions
{

    [Serializable]
    public sealed class UsernotFoundException : Exception
    {
        public readonly string Username;

        public UsernotFoundException(string username) { }
        public UsernotFoundException(string username, string message) : base(message) { }
        public UsernotFoundException(string username, string message, Exception inner) : base(message, inner) { }

        public override string ToString()
        {
            string s = $"User with the name: '{Username}' returned 404";

            if (!string.IsNullOrWhiteSpace(Message))
            {
                s += " Message: " + Message;
            }

            if (InnerException != null)
            {
                s += " Inner exception: " + InnerException.ToString();
            }

            return s;
        }
    }
}
