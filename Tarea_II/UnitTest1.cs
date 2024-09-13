using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;


public interface IList
{
    void InsertInOrder(int value);
    int GetMiddle();
    void MergeSorted(IList listA, IList listB, SortDirection direction);
}

public class Nodo
{
    public int Valor { get; set; }
    public Nodo Siguiente { get; set; }
    public Nodo Anterior { get; set; }

    public Nodo(int valor)
    {
        Valor = valor;
        Siguiente = null;
        Anterior = null;
    }
}

public enum SortDirection
{
    Asc,
    Desc
}

public class ListaDoble : IList
{
    private class Nodo
    {
        public int Valor { get; set; }
        public Nodo Siguiente { get; set; }
        public Nodo Anterior { get; set; }

        public Nodo(int valor)
        {
            Valor = valor;
            Siguiente = null;
            Anterior = null;
        }
    }

    private Nodo Cabeza;
    private Nodo Cola;
    private Nodo Centro;
    private int Tamaño;

    public ListaDoble()
    {
        Cabeza = null;
        Cola = null;
        Centro = null;
        Tamaño = 0;
    }
    // Implementación de MergeSorted
    public void MergeSorted(IList listA, IList listB, SortDirection direction)
    {
        // Validación de listas nulas
        if (listA == null || listB == null)
        {
            throw new ArgumentNullException("Una o ambas listas son nulas.");
        }

        ListaDoble listaA = listA as ListaDoble;
        ListaDoble listaB = listB as ListaDoble;

        if (direction == SortDirection.Asc)
        {
            // Mezcla en orden ascendente
            Nodo actualA = listaA.Cabeza;
            Nodo actualB = listaB.Cabeza;

            while (actualB != null)
            {
                int valor = actualB.Valor;
                InsertInOrderAscendente(listaA, valor);  // Insertar en listaA
                actualB = actualB.Siguiente;
            }
        }
        else if (direction == SortDirection.Desc)
        {
            // Mezcla en orden descendente
            Nodo actualA = listaA.Cabeza;
            Nodo actualB = listaB.Cabeza;

            while (actualB != null)
            {
                int valor = actualB.Valor;
                InsertInOrderDescendente(listaA, valor);  // Insertar en listaA
                actualB = actualB.Siguiente;
            }
        }
    }

    // Métodos de inserción en orden ascendente
    private void InsertInOrderAscendente(ListaDoble lista, int valor)
    {
        Nodo nuevoNodo = new Nodo(valor);

        if (lista.Cabeza == null)
        {
            lista.Cabeza = nuevoNodo;
            lista.Cola = nuevoNodo;
        }
        else
        {
            Nodo actual = lista.Cabeza;
            while (actual != null && actual.Valor < valor)
            {
                actual = actual.Siguiente;
            }

            if (actual == null)
            {
                // Insertar al final
                lista.Cola.Siguiente = nuevoNodo;
                nuevoNodo.Anterior = lista.Cola;
                lista.Cola = nuevoNodo;
            }
            else
            {
                // Insertar antes del actual
                nuevoNodo.Anterior = actual.Anterior;
                nuevoNodo.Siguiente = actual;

                if (actual.Anterior != null)
                {
                    actual.Anterior.Siguiente = nuevoNodo;
                }
                else
                {
                    lista.Cabeza = nuevoNodo;
                }

                actual.Anterior = nuevoNodo;
            }
        }
    }

    // Método de inserción en orden descendente
    private void InsertInOrderDescendente(ListaDoble lista, int valor)
    {
        Nodo nuevoNodo = new Nodo(valor);

        if (lista.Cabeza == null)
        {
            lista.Cabeza = nuevoNodo;
            lista.Cola = nuevoNodo;
        }
        else
        {
            Nodo actual = lista.Cabeza;
            while (actual != null && actual.Valor > valor)
            {
                actual = actual.Siguiente;
            }

            if (actual == null)
            {
                // Insertar al final
                lista.Cola.Siguiente = nuevoNodo;
                nuevoNodo.Anterior = lista.Cola;
                lista.Cola = nuevoNodo;
            }
            else
            {
                // Insertar antes del actual
                nuevoNodo.Anterior = actual.Anterior;
                nuevoNodo.Siguiente = actual;

                if (actual.Anterior != null)
                {
                    actual.Anterior.Siguiente = nuevoNodo;
                }
                else
                {
                    lista.Cabeza = nuevoNodo;
                }

                actual.Anterior = nuevoNodo;
            }
        }
    }
    public void Invertir()
    {
        if (Cabeza == null)
        {
            throw new InvalidOperationException("La lista es nula.");
        }

        Nodo actual = Cabeza;
        Nodo temporal = null;

        // Intercambiar los punteros Siguiente y Anterior para cada nodo
        while (actual != null)
        {
            temporal = actual.Anterior;
            actual.Anterior = actual.Siguiente;
            actual.Siguiente = temporal;
            actual = actual.Anterior;  // Mover hacia el nodo siguiente (que es ahora Anterior)
        }

        // Intercambiar cabeza y cola
        if (temporal != null)
        {
            Cola = Cabeza;
            Cabeza = temporal.Anterior; // La nueva cabeza será el nodo anterior al último nodo procesado
        }
    }

    // Método para insertar elementos en la lista
    public void InsertarAlFinal(int valor)
    {
        Nodo nuevoNodo = new Nodo(valor);
        if (Cabeza == null)
        {
            Cabeza = nuevoNodo;
            Cola = nuevoNodo;
        }
        else
        {
            Cola.Siguiente = nuevoNodo;
            nuevoNodo.Anterior = Cola;
            Cola = nuevoNodo;
        }
    }
    public void InsertInOrder(int valor)
    {
        Nodo nuevoNodo = new Nodo(valor);

        if (Cabeza == null)
        {
            Cabeza = nuevoNodo;
            Cola = nuevoNodo;
            Centro = nuevoNodo;
        }
        else
        {
            Nodo actual = Cabeza;

            // Encontrar la posición correcta para insertar en orden ascendente
            while (actual != null && actual.Valor < valor)
            {
                actual = actual.Siguiente;
            }

            if (actual == null)
            {
                // Insertar al final de la lista
                Cola.Siguiente = nuevoNodo;
                nuevoNodo.Anterior = Cola;
                Cola = nuevoNodo;
            }
            else
            {
                // Insertar antes del nodo actual
                nuevoNodo.Anterior = actual.Anterior;
                nuevoNodo.Siguiente = actual;

                if (actual.Anterior != null)
                {
                    actual.Anterior.Siguiente = nuevoNodo;
                }
                else
                {
                    Cabeza = nuevoNodo; // Si es el primer nodo, se convierte en la nueva cabeza
                }

                actual.Anterior = nuevoNodo;
            }
        }

        Tamaño++;

        // Actualizar el nodo central
        ActualizarCentro();
    }

    // Obtener el elemento central
    public int GetMiddle()
    {
        if (Centro == null)
        {
            throw new InvalidOperationException("La lista está vacía.");
        }

        return Centro.Valor;
    }

    // Método para actualizar el nodo central
    private void ActualizarCentro()
    {
        if (Tamaño == 1)
        {
            Centro = Cabeza;
        }
        else if (Tamaño % 2 == 0)
        {
            Centro = Centro.Siguiente;
        }
        else
        {
            Centro = Centro.Anterior;
        }
    }

    // Método para mostrar los valores de la lista
    public void Mostrar()
    {
        Nodo actual = Cabeza;
        while (actual != null)
        {
            Console.Write(actual.Valor + " ");
            actual = actual.Siguiente;
        }
        Console.WriteLine();
    }
}


namespace ListaDobleTests
{
    [TestClass]
    public class ListaDobleUnitTests
    {
        private ListaDoble lista;

        [TestInitialize]
        public void Setup()
        {
            lista = new ListaDoble();
        }

        // Prueba para InsertInOrder
        [TestMethod]
        public void InsertInOrder_AddsValuesInAscendingOrder()
        {
            lista.InsertInOrder(5);
            lista.InsertInOrder(2);
            lista.InsertInOrder(8);
            lista.InsertInOrder(1);

        }

        // Prueba para DeleteFirst
        [TestMethod]
        public void DeleteFirst_RemovesAndReturnsFirstElement()
        {
            lista.InsertInOrder(10);
            lista.InsertInOrder(20);
            lista.InsertInOrder(30);


        }

        // Prueba para DeleteLast
        [TestMethod]
        public void DeleteLast_RemovesAndReturnsLastElement()
        {
            lista.InsertInOrder(10);
            lista.InsertInOrder(20);
            lista.InsertInOrder(30);


        }

        // Prueba para DeleteValue
        [TestMethod]
        public void DeleteValue_RemovesSpecificElement()
        {
            lista.InsertInOrder(10);
            lista.InsertInOrder(20);
            lista.InsertInOrder(30);


        }

        [TestMethod]
        public void DeleteValue_ReturnsFalseIfNotFound()
        {
            lista.InsertInOrder(10);
            lista.InsertInOrder(20);
            lista.InsertInOrder(30);

        }

        // Prueba para GetMiddle
        [TestMethod]
        public void GetMiddle_ReturnsMiddleElement()
        {
            lista.InsertInOrder(1);
            lista.InsertInOrder(2);
            lista.InsertInOrder(3);

            int middleElement = lista.GetMiddle();

            Assert.AreEqual(2, middleElement);  // El valor medio debería ser 2
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetMiddle_ThrowsExceptionIfEmpty()
        {
            lista.GetMiddle();  // Debería lanzar una excepción si la lista está vacía
        }

        // Prueba para MergeSorted
        [TestMethod]
        public void MergeSorted_MergesTwoSortedListsAscending()
        {
            ListaDoble listA = new ListaDoble();
            listA.InsertInOrder(1);
            listA.InsertInOrder(5);

            ListaDoble listB = new ListaDoble();
            listB.InsertInOrder(2);
            listB.InsertInOrder(3);

            listA.MergeSorted(listA, listB, SortDirection.Asc);

        }

        [TestMethod]
        public void MergeSorted_MergesTwoSortedListsDescending()
        {
            ListaDoble listA = new ListaDoble();
            listA.InsertInOrder(1);
            listA.InsertInOrder(5);

            ListaDoble listB = new ListaDoble();
            listB.InsertInOrder(2);
            listB.InsertInOrder(3);

            listA.MergeSorted(listA, listB, SortDirection.Desc);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MergeSorted_ThrowsExceptionIfListANull()
        {
            ListaDoble listB = new ListaDoble();
            listB.InsertInOrder(2);
            listB.InsertInOrder(3);

            // Debe lanzar una excepción si listA es nulo
            lista.MergeSorted(null, listB, SortDirection.Asc);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MergeSorted_ThrowsExceptionIfListBNull()
        {
            ListaDoble listA = new ListaDoble();
            listA.InsertInOrder(1);
            listA.InsertInOrder(5);

            // Debe lanzar una excepción si listB es nulo
            lista.MergeSorted(listA, null, SortDirection.Asc);
        }
    }
}
