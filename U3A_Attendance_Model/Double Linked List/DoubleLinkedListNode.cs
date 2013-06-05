using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model
{
    public class DoubleLinkedListNode
    {
        /// <summary>
        /// A node for double linked list.
        /// Gets and setts current, next and previous node.
        /// </summary>
        private object element;
        private DoubleLinkedListNode next;
        private DoubleLinkedListNode previous;

        public DoubleLinkedListNode(object element)
        {
            this.element = element;
            this.next = null;
            this.previous = null;
        }

        public DoubleLinkedListNode(object element, DoubleLinkedListNode prevNode)
        {
            this.element = element;
            this.previous = prevNode;
            prevNode.next = this;
        }

        public object Element
        {
            get { return this.element; }
            set { this.element = value; }
        }

        public DoubleLinkedListNode Next
        {
            get { return this.next; }
            set { this.next = value; }
        }

        public DoubleLinkedListNode Previous
        {
            get { return this.previous; }
            set { this.previous = value; }
        }
    }
}
