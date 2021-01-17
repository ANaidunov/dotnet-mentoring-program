using System;
using System.Collections;
using System.Collections.Generic;

namespace Tasks
{
    public class DoublyLinkedListEnumerator<T> : IEnumerator<T>
    {
        private DoublyLinkedNode<T> _current;
        private readonly DoublyLinkedNode<T> _start;
        private bool _startedMoveFlag;

        public DoublyLinkedListEnumerator(DoublyLinkedNode<T> current)
        {
            _current = current;
            _start = current;
        }

        public T Current => _current.Data;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            _current = null;
        }

        public bool MoveNext()
        {
            if (_startedMoveFlag == false)
            {
                _current = _start;
                _startedMoveFlag = true;
            }
            else
            {
                _current = _current.Next;
            }

            return _current != null;
        }

        public void Reset()
        {
            _current = _start;
        }
    }
}
