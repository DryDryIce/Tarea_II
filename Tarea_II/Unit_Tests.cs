using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkedListTests
{
    [TestClass]
    public class DoublyLinkedListTests
    {
        [TestMethod]
        public void AddLast_ShouldInsertAtEnd()
        {
            // Arrange
            ListaDoble list = new ListaDoble();

            // Act
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);

            // Assert
            Assert.AreEqual(1, list.Head.Value);
            Assert.AreEqual(3, list.Tail.Value);
        }

        [TestMethod]
        public void GetMiddle_ShouldReturnMiddleValue()
        {
            // Arrange
            ListaDoble list = new ListaDoble();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(4);
            list.AddLast(5);

            // Act
            int middleValue = list.GetMiddle();

            // Assert
            Assert.AreEqual(3, middleValue);  // El valor del medio es 3
        }

        [TestMethod]
        public void InsertInOrder_ShouldMaintainSortedOrder()
        {
            // Arrange
            ListaDoble list = new ListaDoble();

            // Act
            list.InsertInOrder(5);
            list.InsertInOrder(1);
            list.InsertInOrder(3);

            // Assert
            Assert.AreEqual(1, list.Head.Value);
            Assert.AreEqual(5, list.Tail.Value);
        }

        [TestMethod]
        public void MergeSorted_ShouldMergeInAscendingOrder()
        {
            // Arrange
            ListaDoble listA = new ListaDoble();
            ListaDoble listB = new ListaDoble();
            listA.AddLast(1);
            listA.AddLast(5);
            listA.AddLast(10);

            listB.AddLast(2);
            listB.AddLast(6);
            listB.AddLast(12);

            // Act
            ListaDoble.MergeSorted(listA, listB, SortDirection.Asc);

            // Assert
            Assert.AreEqual(1, listA.Head.Value);
            Assert.AreEqual(12, listA.Tail.Value);
            Assert.AreEqual(6, listA.GetMiddle());  // El valor central debería ser 6
        }

        [TestMethod]
        public void MergeSorted_ShouldMergeInDescendingOrder()
        {
            // Arrange
            ListaDoble listA = new ListaDoble();
            ListaDoble listB = new ListaDoble();
            listA.AddLast(1);
            listA.AddLast(5);
            listA.AddLast(10);

            listB.AddLast(2);
            listB.AddLast(6);
            listB.AddLast(12);

            // Act
            ListaDoble.MergeSorted(listA, listB, SortDirection.Desc);

            // Assert
            Assert.AreEqual(12, listA.Head.Value);
            Assert.AreEqual(1, listA.Tail.Value);
            Assert.AreEqual(5, listA.GetMiddle());  // El valor central debería ser 6 después de invertir
        }

        [TestMethod]
        public void Invert_ShouldReverseTheList()
        {
            // Arrange
            ListaDoble list = new ListaDoble();
            list.AddLast(1);
            list.AddLast(2);
            list.AddLast(3);
            list.AddLast(4);
            list.AddLast(5);

            // Act
            list.Invert();

            // Assert
            Assert.AreEqual(5, list.Head.Value);
            Assert.AreEqual(1, list.Tail.Value);
            Assert.AreEqual(3, list.GetMiddle());  // El valor del medio después de invertir sigue siendo 3
        }
    }
}
