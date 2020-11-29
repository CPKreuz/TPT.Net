using System;
using System.Collections.Generic;
using System.Text;

namespace TPT.Exceptions
{
    /// <summary>
    /// Throwen when the API enpoint returns 404
    /// </summary>
    [Serializable]
    public sealed class SaveNotFoundException : Exception
    {
        public readonly int Id;

        public SaveNotFoundException(int Id) { this.Id = Id; }
        public SaveNotFoundException(int Id, string message) : base(message) { this.Id = Id; }
        public SaveNotFoundException(int Id, string message, Exception inner) : base(message, inner) { this.Id = Id; }

        public override string ToString()
        {
            string s = $"Save with the ID: '{Id}' returned 404";

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
