using System;
using System.Collections;
using System.Collections.Generic;

namespace Enigma
{
    public class List<T> :IList<T>
    {
        int lenght;
        internal Node<T> first;
        Node<T> last;
        public int Count => lenght;
        public bool IsReadOnly => false;
        public T this[int key] { get => GetNode(key).Get(); set => Set(key, value); }
        private Node <T> GetNode(int index)
            {
                if (lenght == 0 || index >= lenght || index < 0) throw new IndexOutOfRangeException();
                if (index > (lenght / 2))
                {
                    Node<T> current = last;
                    for (int i = 0; i < lenght - index - 1; i++)
                    {
                        current = current.prev;
                    }
                    return current;
                }
                else
                {
                    Node<T> current = first;
                    for (int i = 0; i < index; i++)
                    {
                        current = current.next;
                    }
                    return current;
                }
            }
        private void Set(int index, T value)
        {
            if (index > (lenght / 2))
            {
                Node<T> current = last;
                for (int i = 0; i < lenght - index - 1; i++)
                {
                    current = current.prev;
                }
                current.Set(value);
                return;
            }
            else
            {
                Node<T> current = first;
                for (int i = 0; i < index; i++)
                {
                    current = current.next;
                }
                current.Set(value);
                return;
            }
        }
        public void Add(T a)
        {
            if (lenght == 0)
            {
                first = new Node<T>(a);
                last = first;
            }
            else
            {
                last.next = new Node<T>(a);
                last.next.prev = last;
                last = last.next;
            }
            lenght++;
        }

        public int IndexOf(T item)
        {
            for(int i = 0; i < lenght; i++)
            {
                if (this[i].Equals(item)) return i;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index == 0 && lenght == 0)
            {
                Add(item);
            }
            else if (lenght == 0 || index > lenght || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            if (index == lenght)
            {
                Add(item);
            }
            else if(index == 0)
            {
                var next = first;
                var newNode = new Node<T>(item);
                lenght++;
                next.prev = newNode;
                newNode.next = first;
                first = newNode;
            }
            else
            {
                var prev = GetNode(index - 1);
                var next = prev.next;
                var newNode = new Node<T>(item);
                lenght++;
                next.prev = newNode;
                prev.next = newNode;
                newNode.next = next;
                newNode.prev = prev;
            }
        }

        public void RemoveAt(int index)
        {
            if (lenght == 0 || index >= lenght || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            else if (index == 0)
            {
                var next = first.next;
                first = next;
                first.prev = null;
                lenght--;
            }
            else if(index == lenght - 1)
            {
                var prev = last.prev;
                last = prev;
                last.next = null;
                lenght--;
            }
            else
            {
                var next = GetNode(index).next;
                var prev = next.prev.prev;
                next.prev = prev;
                prev.next = next;
                lenght--;
            }
        }

        public void Clear()
        {
            first = null;
            last = null;
            lenght = 0;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach(T item in this)
            {
                array[arrayIndex] = item;
                arrayIndex++;
            }
        }

        public bool Remove(T item)
        {
            for (int i = 0; i < lenght; i++)
            {
                if (GetNode(i).Get().Equals(item))
                {
                    RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ListEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ListEnumerator<T>(this);
        }
    }
    class Node<T>
    {

        T value;
        public Node(T a)
        {
            value = a;
        }
        public T Get()
        {
            return value;
        }
        public void Set(T ind)
        {
            value = ind;
        }
        public Node<T> next;
        public Node<T> prev;
    }
    class ListEnumerator<T> : IEnumerator<T>
    {
        private Node<T> CurrentNode;
        private List<T> ListCurrent;

        public ListEnumerator(List<T> currentList)
        {
            ListCurrent = currentList;
            CurrentNode = null;
        }

        public T Current
        {
            get
            {
                if(CurrentNode == null)
                {
                    return default(T);
                }
                else
                {
                    return CurrentNode.Get();
                }
            }
            set
            {
                if(CurrentNode != null)
                {
                    CurrentNode.Set(value);
                }
            }
        }

        object IEnumerator.Current
        {
            get
            {
                if (CurrentNode == null)
                {
                    return default(T);
                }
                else
                {
                    return CurrentNode.Get();
                }
            }
        }

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            if(CurrentNode == null)
            {
                CurrentNode = ListCurrent.first;
                return CurrentNode != null;
            }
            if (CurrentNode.next != null)
            {
                CurrentNode = CurrentNode.next;
                return true;
            }
            else return false;
        }

        public void Reset()
        {
            CurrentNode = ListCurrent.first;
        }
    }
}