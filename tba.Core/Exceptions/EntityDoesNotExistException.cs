using System;

namespace tba.Core.Exceptions
{
    public class EntityDoesNotExistException : Exception
    {
        public EntityDoesNotExistException()
        {

        }

        public EntityDoesNotExistException(string message)
            : base(message)
        {

        }

        public EntityDoesNotExistException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}