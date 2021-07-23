using System;
using System.Collections.Generic;

namespace TaskManagerLibrary
{
    public interface IAssignable
    {
        List<User> Executors { get; }

        void AddExecutor(User executor);
        void RemoveExecutor(User executor);

        /// <summary>
        /// Maximum of executors.
        /// </summary>
        public int Size { get; set; }
    }
}
