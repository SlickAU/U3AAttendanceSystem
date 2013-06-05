using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model
{
    public class DoubleLinkedList<T> : IEnumerable<T>
    {
        /// <summary>
        /// Base class for Double Linked List
        /// Inherits from Ienumerable for enumeration and extended functionality
        /// </summary>
        private DoubleLinkedListNode current;
        private DoubleLinkedListNode previous;
        private int count;

        //List needed for enumeration and easy accessable display of results 
        List<T> _list = new List<T>();

        public DoubleLinkedList()
        {
            this.current = null;
            this.previous = null;
            this.count = 0;
        }

        public DoubleLinkedList(List<T> list)
        {
            foreach (var i in list)
                Add(i);
        }

        public int Count
        {
            get { return this.count; }
        }

        public object this[int index]
        {
            get
            {
                if (index >= count || index < 0)
                {
                    throw new ArgumentOutOfRangeException("Out of range!");
                }

                DoubleLinkedListNode currentNode = this.current;

                for (int i = 0; i < index; i++)
                {
                    currentNode = currentNode.Next;
                }
                return currentNode.Element;
            }
            set
            {
                if (index >= count || index < 0)
                {
                    throw new ArgumentOutOfRangeException("Out of range!");
                }
                DoubleLinkedListNode currentNode = this.current;
                for (int i = 0; i < index; i++)
                {
                    currentNode = currentNode.Next;
                }
                currentNode.Element = value;
            }
        }

        public void Add(object item)
        {
            if (this.current == null)
            {
                this.current = new DoubleLinkedListNode(item);
                this.previous = this.current;
                _list.Add((T)item);
            }
            else
            {
                DoubleLinkedListNode newItem = new DoubleLinkedListNode(item, previous);
                this.previous = newItem;
                _list.Add((T)item);
            }
            count++;
        }

        //Here we consider two cases: when the list is empty and when the list is not empty. 
        //The purpose here is to add every new node in the end of the list and after adding every variable (current, previous and count) to have correct values.
        //Insert a node in the list (Insert(object, position)).
        //Insert current node on the current position. Here we increase count, make our new element and searching his position in the list. 
        //When the current position is found and after some checks about where we want to add the new element we redirect his next and previous pointers to his next and previous nodes.
        public void Insert(object item, int index)
        {
            count++;
            if (index >= count || index < 0)
            {
                throw new ArgumentOutOfRangeException("Out of range!");
            }
            DoubleLinkedListNode newItem = new DoubleLinkedListNode(item);
            int currentIndex = 0;
            DoubleLinkedListNode currentItem = this.current;
            DoubleLinkedListNode prevItem = null;
            while (currentIndex < index)
            {
                prevItem = currentItem;
                currentItem = currentItem.Next;
                currentIndex++;
            }
            if (index == 0)
            {
                newItem.Previous = this.current.Previous;
                newItem.Next = this.current;
                this.current.Previous = newItem;
                this.current = newItem;
            }
            else if (index == count - 1)
            {
                newItem.Previous = this.previous;
                this.previous.Next = newItem;
                newItem = this.previous;
            }
            else
            {
                newItem.Next = prevItem.Next;
                prevItem.Next = newItem;
                newItem.Previous = currentItem.Previous;
                currentItem.Previous = newItem;
            }
        }
        
        //First we check if the current node exist and if the node exists remove it.
        //Remove a node from list by element (Remove(object)).
        public void Remove(object item)
        {
            int currentIndex = 0;
            DoubleLinkedListNode currentItem = this.current;
            DoubleLinkedListNode prevItem = null;
            while (currentItem != null)
            {
                if ((currentItem.Element != null &&
                    currentItem.Element.Equals(item)) ||
                    (currentItem.Element == null) && (item == null))
                {
                    break;
                }
                prevItem = currentItem;
                currentItem = currentItem.Next;
                currentIndex++;
            }
            if (currentItem != null)
            {
                count--;
                if (count == 0)
                {
                    this.current = null;
                }
                else if (prevItem == null)
                {
                    this.current = currentItem.Next;
                    this.current.Previous = null;
                }
                else if (currentItem == previous)
                {
                    currentItem.Previous.Next = null;
                    this.previous = currentItem.Previous;
                }
                else
                {
                    currentItem.Previous.Next = currentItem.Next;
                    currentItem.Next.Previous = currentItem.Previous;
                }
            }
        }

        //Remove a node from list by index is almost the same like remove a node from list by element only with one small difference.
        //We have to check if the index exists and if it doesn’t exist throw an appropriate exception.
        //Remove a node from list by index (RemoveAt(index)).
        public void RemoveAt(int index)
        {
            if (index >= this.count || index < 0)
            {
                throw new ArgumentOutOfRangeException("Out of range!");
            }

            int currentIndex = 0;
            DoubleLinkedListNode currentItem = this.current;
            DoubleLinkedListNode prevItem = null;
            while (currentIndex < index)
            {
                prevItem = currentItem;
                currentItem = currentItem.Next;
                currentIndex++;
            }
            if (this.count == 0)
            {
                this.current = null;
            }
            else if (prevItem == null)
            {
                this.current = currentItem.Next;
                this.current.Previous = null;
            }
            else if (index == count - 1)
            {
                prevItem.Next = currentItem.Next;
                previous = prevItem;
                currentItem = null;
            }
            else
            {
                prevItem.Next = currentItem.Next;
                currentItem.Next.Previous = prevItem;
            }
            count--;
        }
        

        //Return index of element (IndexOf(object)).
        //Search for given element in the list. And if the element is found return index of element or if element is not found return -1;
        public int IndexOf(object item)
        {
            int index = 0;
            DoubleLinkedListNode currentItem = this.current;
            while (currentItem != null)
            {
                if (((currentItem.Element != null) && (item == currentItem.Element)) ||
                ((currentItem.Element == null) && (item == null)))
                {
                    return index;
                }
                index++;
                currentItem = currentItem.Next;
            }
            return -1;
        }
        

        //Checks whether element exist (Contains(object)).
        //Here we call the IndexOf(object) and if the return index is different by -1 this 
        //means that the element which we search exist and method returns true else returns false if the element doesn’t exist.
        public bool Contains(object element)
        {
            int index = IndexOf(element);
            bool contains = (index != -1);
            return contains;
        }
       

        //Clear the list (Clear()).
        public void Clear()
        {
            this.current = null;
            this.previous = null;
            this.count = 0;
        }
        

        //Enumerates the list
        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
           return GetEnumerator();
        }

    }
}
        
