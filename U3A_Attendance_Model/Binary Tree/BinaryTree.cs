using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U3A_Attendance_Model
{
    public class BinaryTree<T> where T : IComparable<T>
    {


        private T personValue;

        public BinaryTree()
        {
            this.Left = null;
            this.Right = null;
        }

        public BinaryTree(T person)
        {
            this.PersonValue = person;
            this.Left = null;
            this.Right = null;
        }

        #region Properties


        public T PersonValue
        {
            get
            {
                return personValue;
            }
            set
            {
                personValue = value;              
                RootNodeInserted = true;
            }
        }
        public bool RootNodeInserted { get; set; }
        public BinaryTree<T> Left { get; set; }
        public BinaryTree<T> Right { get; set; }

        #endregion

        #region Methods

        public void Insert(T person)
        {
            T currentPersonValue = this.PersonValue;

            if (currentPersonValue.CompareTo(person) > 0)
            {
                if (this.Left == null)
                {
                    this.Left = new BinaryTree<T>(person);
                }
                else
                {
                    this.Left.Insert(person);
                }
            }
            else
            {
                if (this.Right == null)
                {
                    this.Right = new BinaryTree<T>(person);
                }
                else
                {
                    this.Right.Insert(person);
                }
            }
        }

        public void WalkTree()
        {
            if (this.Left != null)
            {
                this.Left.WalkTree();
            }

            if (this.Right != null)
            {
                this.Right.WalkTree();
            }
        }


        #endregion

    }

}
