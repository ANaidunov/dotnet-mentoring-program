using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private DoublyLinkedNode<T> _head;
        private DoublyLinkedNode<T> _tail;
        private int _size;
        public int Length => _size;

        public void Add(T e)
        {
            var newNode = new DoublyLinkedNode<T>(e);
            if (_head is null)
            {
                _head = newNode;
            }
            else
            {
                _tail.Next = newNode;
                newNode.Prev = _tail;
            }

            _tail = newNode;
            _size++;
        }

        public void AddAt(int index, T e)
        {
            if (index < 0 || index > _size)
            {
                throw new ArgumentException("index should be from 0 to index of last element + 1", nameof(index));
            }

            var nodeToAdd = new DoublyLinkedNode<T>(e);
            if (index == 0)
            {
                AddFirst(nodeToAdd);
            }
            else if (index == _size)
            {
                AddLast(nodeToAdd);
            }
            else
            {
                var placeToAdd = SearchNodeByIndex(index);

                nodeToAdd.Prev = placeToAdd.Prev;
                nodeToAdd.Next = placeToAdd;

                placeToAdd.Prev = nodeToAdd;
                nodeToAdd.Prev.Next = nodeToAdd;
            }

            _size++;
        }

        public T ElementAt(int index)
        {
            CheckIndexForOutOfRange(index);

            var node = SearchNodeByIndex(index);

            return node.Data;
        }
        public IEnumerator<T> GetEnumerator()
        {
            return new DoublyLinkedListEnumerator<T>(_head);
        }

        public void Remove(T item)
        {
            var current = _head;
            while (current != null)
            {
                if (current.Data.Equals(item))
                {
                    _size--;
                    break;
                }

                current = current.Next;
            }

            if (current != null)
            {
                if (current.Next != null)
                {
                    current.Next.Prev = current.Prev;
                }
                else
                {
                    _tail = current.Prev;
                }

                if (current.Prev != null)
                {
                    current.Prev.Next = current.Next;
                }
                else
                {
                    _head = current.Next;
                }
            }
        }

        public T RemoveAt(int index)
        {
            CheckIndexForOutOfRange(index);
            var removedNode = SearchNodeByIndex(index);

            if (index == 0)
            {
                _head = _head.Next;
                _head.Prev = null;

            }
            else if (index == _size - 1)
            {
                _tail = _tail.Prev;
                _tail.Next = null;
            }
            else
            {
                removedNode.Prev.Next = removedNode.Next;
                removedNode.Next.Prev = removedNode.Prev;
            }

            _size--;
            return removedNode.Data;
        }

        private void AddFirst(DoublyLinkedNode<T> node)
        {
            var temp = _head;
            node.Next = temp;
            _head = node;
            if (_size == 0)
                _tail = _head;
            else
                temp.Prev = node;
            _size++;
        }

        private void AddLast(DoublyLinkedNode<T> node)
        {
            _tail.Next = node;
            node.Prev = _tail;
            _tail = node;
        }

        private void CheckIndexForOutOfRange(int index)
        {
            if (index < 0 || index >= _size)
            {
                throw new IndexOutOfRangeException();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private DoublyLinkedNode<T> SearchNodeByIndex(int index)
        {
            var isSearchFromHead = index <= _size / 2;
            var current = isSearchFromHead ? StartSearchFromHead(index) : StartSearchFromTail(index);
            return current;
        }

        private DoublyLinkedNode<T> StartSearchFromHead(int index)
        {
            int tempIndex = 0;
            var current = _head;
            while (tempIndex < index)
            {
                current = current.Next;
                tempIndex++;
            }

            return current;
        }

        private DoublyLinkedNode<T> StartSearchFromTail(int index)
        {
            int tempIndex = _size;
            var current = _tail;
            while (tempIndex < index)
            {
                current = current.Prev;
                tempIndex--;
            }

            return current;
        }
    }
}
