using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatchLibrary;
using System.Collections.Generic;
using LAB12;

namespace LAB12
{
    [TestClass]
    public class BalancedBinaryTreeTests
    {
        [TestMethod]
        public void CreateBalancedTree_WithValidList_CreatesCorrectTreeStructure()
        {
            // Arrange
            var tree = new BalancedBinaryTree();
            var watches = new List<Watch>
            {
                new Watch("Brand1", 2000),
                new Watch("Brand2", 1995),
                new Watch("Brand3", 2010),
                new Watch("Brand4", 1985),
                new Watch("Brand5", 2005)
            };

            // Act
            tree.CreateBalancedTree(watches);

            // Assert
            Assert.AreEqual(2000, tree.root.Data.YearOfManufacture);
            Assert.AreEqual(1995, tree.root.Left.Data.YearOfManufacture);
            Assert.AreEqual(2010, tree.root.Right.Data.YearOfManufacture);
            Assert.AreEqual(1985, tree.root.Left.Left.Data.YearOfManufacture);
            Assert.AreEqual(2005, tree.root.Right.Left.Data.YearOfManufacture);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateBalancedTree_WithEmptyList_ThrowsArgumentException()
        {
            // Arrange
            var tree = new BalancedBinaryTree();
            var watches = new List<Watch>();

            // Act
            tree.CreateBalancedTree(watches);
        }

        [TestMethod]
        public void FindMinYear_ReturnsCorrectMinimumYear()
        {
            // Arrange
            var tree = new BalancedBinaryTree();
            var watches = new List<Watch>
            {
                new Watch("Brand1", 2000),
                new Watch("Brand2", 1995),
                new Watch("Brand3", 2010),
                new Watch("Brand4", 1985),
                new Watch("Brand5", 2005)
            };
            tree.CreateBalancedTree(watches);

            // Act
            var result = tree.FindMinYear();

            // Assert
            Assert.AreEqual(1985, result.YearOfManufacture);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FindMinYear_OnEmptyTree_ThrowsInvalidOperationException()
        {
            // Arrange
            var tree = new BalancedBinaryTree();

            // Act
            tree.FindMinYear();
        }

        [TestMethod]
        public void ConvertToSearchTree_CreatesValidSearchTree()
        {
            // Arrange
            var tree = new BalancedBinaryTree();
            var watches = new List<Watch>
            {
                new Watch("Brand1", 2000),
                new Watch("Brand2", 1995),
                new Watch("Brand3", 2010),
                new Watch("Brand4", 1985),
                new Watch("Brand5", 2005)
            };
            tree.CreateBalancedTree(watches);

            // Act
            var searchTree = tree.ConvertToSearchTree();

            // Assert
            Assert.AreEqual(1985, searchTree.root.Data.YearOfManufacture);
            Assert.AreEqual(1995, searchTree.root.Right.Data.YearOfManufacture);
            Assert.AreEqual(2000, searchTree.root.Right.Right.Data.YearOfManufacture);
            Assert.AreEqual(2005, searchTree.root.Right.Right.Right.Data.YearOfManufacture);
            Assert.AreEqual(2010, searchTree.root.Right.Right.Right.Right.Data.YearOfManufacture);
        }

        [TestMethod]
        public void DeleteNode_LeafNode_RemovesNodeCorrectly()
        {
            // Arrange
            var tree = new BalancedBinaryTree();
            var watches = new List<Watch>
            {
                new Watch("Brand1", 2000),
                new Watch("Brand2", 1995),
                new Watch("Brand3", 2010),
                new Watch("Brand4", 1985),
                new Watch("Brand5", 2005)
            };
            tree.CreateBalancedTree(watches);

            // Act
            tree.DeleteNode(1985);

            // Assert
            Assert.IsNull(tree.root.Left.Left);
        }

        [TestMethod]
        public void DeleteNode_NodeWithOneChild_RemovesNodeCorrectly()
        {
            // Arrange
            var tree = new BalancedBinaryTree();
            var watches = new List<Watch>
            {
                new Watch("Brand1", 2000),
                new Watch("Brand2", 1995),
                new Watch("Brand3", 2010),
                new Watch("Brand4", 1985),
                new Watch("Brand5", 2005)
            };
            tree.CreateBalancedTree(watches);

            // Act
            tree.DeleteNode(2010);

            // Assert
            Assert.AreEqual(2005, tree.root.Right.Data.YearOfManufacture);
        }

        [TestMethod]
        public void DeleteNode_NodeWithTwoChildren_RemovesNodeCorrectly()
        {
            // Arrange
            var tree = new BalancedBinaryTree();
            var watches = new List<Watch>
            {
                new Watch("Brand1", 2000),
                new Watch("Brand2", 1995),
                new Watch("Brand3", 2010),
                new Watch("Brand4", 1985),
                new Watch("Brand5", 2005),
                new Watch("Brand6", 2015)
            };
            tree.CreateBalancedTree(watches);

            // Act
            tree.DeleteNode(2000);

            // Assert
            Assert.AreEqual(2005, tree.root.Data.YearOfManufacture);
        }

        [TestMethod]
        public void ClearTree_RemovesAllNodes()
        {
            // Arrange
            var tree = new BalancedBinaryTree();
            var watches = new List<Watch>
            {
                new Watch("Brand1", 2000),
                new Watch("Brand2", 1995),
                new Watch("Brand3", 2010)
            };
            tree.CreateBalancedTree(watches);

            // Act
            tree.ClearTree();

            // Assert
            Assert.IsNull(tree.root);
        }

        [TestMethod]
        public void PrintTreeByLevels_EmptyTree_PrintsEmptyMessage()
        {
            // Arrange
            var tree = new BalancedBinaryTree();
            var consoleOutput = new System.IO.StringWriter();
            System.Console.SetOut(consoleOutput);

            // Act
            tree.PrintTreeByLevels();

            // Assert
            StringAssert.Contains(consoleOutput.ToString(), "Дерево пустое");
        }
    }
    [TestClass]
    public class MyCollectionTests
    {
        [TestMethod]
        public void TestMyCollection_AddAndCount()
        {
            var collection = new MyCollection<Watch>();
            collection.Add(new Watch("Rolex", 2020));
            collection.Add(new AnalogWatch("Casio", 2021, "Sport"));

            Assert.AreEqual(2, collection.Count);
        }

        [TestMethod]
        public void TestMyCollection_Remove()
        {
            var watch = new ElectronicWatch("Sony", 2022, "OLED");
            var collection = new MyCollection<Watch> { watch };

            bool removed = collection.Remove(watch);

            Assert.IsTrue(removed);
            Assert.AreEqual(0, collection.Count);
        }

        [TestMethod]
        public void TestMyCollection_Contains()
        {
            var watch = new SmartWatch("Apple", 2023, "AMOLED", "WatchOS", true);
            var collection = new MyCollection<Watch> { watch };

            bool contains = collection.Contains(watch);

            Assert.IsTrue(contains);
        }
    }
    [TestClass]
    public class MyTreeCollectionTests
    {
        [TestMethod]
        public void TestMyTreeCollection_AddAndTraverse()
        {
            var tree = new MyTreeCollection<Watch>();
            tree.Add(new Watch("A", 2000));
            tree.Add(new Watch("B", 1999));
            tree.Add(new Watch("C", 2001));

            var years = tree.Select(w => w.YearOfManufacture).ToList();

            CollectionAssert.AreEqual(new[] { 1999, 2000, 2001 }, years);
        }

        [TestMethod]
        public void TestMyTreeCollection_Remove()
        {
            var watch = new AnalogWatch("Vostok", 1985, "Military");
            var tree = new MyTreeCollection<Watch>();
            tree.Add(watch);

            bool removed = tree.Remove(watch);

            Assert.IsTrue(removed);
            Assert.AreEqual(0, tree.Count());
        }

        [TestMethod]
        public void TestMyTreeCollection_ConstructorWithLength()
        {
            var tree = new MyTreeCollection<ElectronicWatch>(5);

            Assert.AreEqual(5, tree.Count());
        }
    }

    [TestClass]
    public class MyHashCollectionTests
    {
        [TestMethod]
        public void TestMyHashCollection_AddAndGet()
        {
            var hash = new MyHashCollection<Watch>();
            hash.Add("rolex", new Watch("Rolex", 2020));

            Assert.AreEqual(2020, hash["rolex"].YearOfManufacture);
        }

        [TestMethod]
        public void TestMyHashCollection_KeyCollision()
        {
            var hash = new MyHashCollection<Watch>();
            hash.Add("casio", new Watch("Casio", 2019));

            // Должно перезаписать предыдущее значение
            hash.Add("casio", new Watch("Casio", 2020));

            Assert.AreEqual(2020, hash["casio"].YearOfManufacture);
        }

        [TestMethod]
        public void TestMyHashCollection_Remove()
        {
            var hash = new MyHashCollection<SmartWatch>();
            hash.Add("apple", new SmartWatch("Apple", 2023, "OLED", "WatchOS", true));

            bool removed = hash.Remove("apple");

            Assert.IsTrue(removed);
            Assert.IsFalse(hash.ContainsKey("apple"));
        }
    }
}