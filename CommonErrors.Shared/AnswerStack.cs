using System.Collections.Generic;

namespace CommonErrorsKata.Shared
{
    public class AnswerStack<T> : Stack<T> where T: IGradable
    {
        private readonly int size;

        /// <summary>
        /// Stack that cannot exceed it's size
        /// </summary>
        /// <param name="size">Maximum size of the stack</param>
        public AnswerStack(int size)
        {
            this.size = size;
        }

        /// <summary>
        /// Hides the default implementation of Stack push 
        /// </summary>
        /// <param name="item"></param>
        public new void Push(T item)
        {
            this.Push(item);
        }

        /// <summary>
        /// Hides the default implementation of Stack Add
        /// </summary>
        /// <param name="item">Item to add to the size</param>
        public void Add(T item)
        {
            this.Add(item);
        }
        
    }
}
