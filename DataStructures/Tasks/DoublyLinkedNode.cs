namespace Tasks
{
    public class DoublyLinkedNode<T>
    {
        public DoublyLinkedNode(T data)
        {
            Data = data;
        }
        public T Data { get; set; }
        public DoublyLinkedNode<T> Prev;
        public DoublyLinkedNode<T> Next;
    }
}
