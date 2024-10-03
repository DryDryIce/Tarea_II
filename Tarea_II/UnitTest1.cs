using System;

namespace LinkedListTests
{
    public enum SortDirection
    {
        Asc,    // Ascendente
        Desc    // Descendente
    }

    public class DoublyLinkedListNode
    {
        public int Value { get; set; }
        public DoublyLinkedListNode Next { get; set; }
        public DoublyLinkedListNode Prev { get; set; }

        public DoublyLinkedListNode(int value)
        {
            Value = value;
            Next = null;
            Prev = null;
        }
    }

    public class ListaDoble
    {
        public DoublyLinkedListNode Head { get; private set; }
        public DoublyLinkedListNode Tail { get; private set; }
        private DoublyLinkedListNode Middle { get; set; }  // Referencia al nodo medio
        private int Count { get; set; }  // Contador de nodos

        public ListaDoble()
        {
            Head = null;
            Tail = null;
            Middle = null;
            Count = 0;
        }

        // Inserta un valor al final de la lista y actualiza el nodo central
        public void AddLast(int value)
        {
            DoublyLinkedListNode newNode = new DoublyLinkedListNode(value);
            if (Head == null)
            {
                Head = Tail = Middle = newNode;
            }
            else
            {
                Tail.Next = newNode;
                newNode.Prev = Tail;
                Tail = newNode;
            }

            Count++;

            // Actualizar el nodo medio si el tamaño es par
            if (Count % 2 == 0)
            {
                Middle = Middle.Next;
            }
        }

        public void InsertInOrder(int value)
        {
            DoublyLinkedListNode newNode = new DoublyLinkedListNode(value);

            if (Head == null)
            {
                Head = Tail = Middle = newNode;
            }
            else if (value < Head.Value)
            {
                newNode.Next = Head;
                Head.Prev = newNode;
                Head = newNode;
            }
            else
            {
                DoublyLinkedListNode current = Head;
                while (current.Next != null && current.Next.Value < value)
                {
                    current = current.Next;
                }

                newNode.Next = current.Next;
                if (current.Next != null)
                {
                    current.Next.Prev = newNode;
                }
                else
                {
                    Tail = newNode;
                }
                newNode.Prev = current;
                current.Next = newNode;
            }

            Count++;

            // Actualizar el nodo medio si el tamaño es par
            if (Count % 2 == 0)
            {
                Middle = Middle.Next;
            }
        }


        // Método para mezclar dos listas en orden ascendente o descendente y actualizar el nodo central
        public static void MergeSorted(ListaDoble listA, ListaDoble listB, SortDirection direction)
        {
            if (listA == null || listB == null)
            {
                throw new ArgumentNullException("Las listas no pueden ser null.");
            }

            DoublyLinkedListNode currentB = listB.Head;

            while (currentB != null)
            {
                listA.InsertInOrder(currentB.Value);
                currentB = currentB.Next;
            }

            if (direction == SortDirection.Desc)
            {
                listA.Reverse();
            }

            // Recalcular el nodo central después de la fusión
            listA.UpdateMiddle();
        }

        // Método para actualizar el nodo central basado en el tamaño actual de la lista
        private void UpdateMiddle()
        {
            if (Count == 0)
            {
                Middle = null;
            }
            else
            {
                Middle = Head;
                int middlePosition = Count / 2;
                for (int i = 0; i < middlePosition; i++)
                {
                    Middle = Middle.Next;
                }
            }
        }

        // Método para invertir la lista (útil para el orden descendente)
        public void Reverse()
        {
            DoublyLinkedListNode current = Head;
            DoublyLinkedListNode temp = null;

            // Intercambiar el siguiente y el anterior para cada nodo
            while (current != null)
            {
                temp = current.Prev;
                current.Prev = current.Next;
                current.Next = temp;
                current = current.Prev;
            }

            // Ajustar la cabeza y la cola
            if (temp != null)
            {
                Tail = Head;  // La cola anterior es la nueva cabeza
                Head = temp.Prev;
            }

            // Recalcular el nodo central después de la inversión
            UpdateMiddle();
        }


        // Método para obtener el elemento central
        public int GetMiddle()
        {
            if (Head == null)
            {
                throw new InvalidOperationException("La lista está vacía.");
            }
            return Middle.Value;
        }


        // Método para imprimir la lista (para propósitos de prueba)
        public void PrintList()
        {
            DoublyLinkedListNode current = Head;
            while (current != null)
            {
                Console.Write(current.Value + " ");
                current = current.Next;
            }
            Console.WriteLine();
        }

        // Método para invertir la lista
        public void Invert()
        {
            if (Head == null)
            {
                throw new InvalidOperationException("La lista está vacía.");
            }

            DoublyLinkedListNode current = Head;
            DoublyLinkedListNode temp = null;

            while (current != null)
            {
                temp = current.Prev;
                current.Prev = current.Next;
                current.Next = temp;
                current = current.Prev;
            }

            if (temp != null)
            {
                Tail = Head;  // La cola anterior se convierte en la nueva cabeza
                Head = temp.Prev;  // La nueva cabeza es el nodo anterior
            }

            // Actualizar el nodo medio tras la inversión
            UpdateMiddle();
        }
    }
}