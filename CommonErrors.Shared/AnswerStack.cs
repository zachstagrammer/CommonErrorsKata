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
            if (this.Count < size)
                base.Push(item);
        }

        
        
    }
}
