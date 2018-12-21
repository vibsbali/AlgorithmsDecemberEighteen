using Algorithms.Trees;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithms.UnitTests
{
    [TestClass]
    public class BinarySearchTreeTests
    {
        [TestMethod]
        public void EmptyTreeAssertCountZero()
        {
            var bst = new BinarySearchTree<int>();
            Assert.AreEqual(0, bst.Count);
        }

        [TestMethod]
        public void TreeAdd500AssertCountOneHeadValue500()
        {
            var bst = new BinarySearchTree<int>();
            bst.Add(500);

            Assert.AreEqual(1, bst.Count);
            Assert.AreEqual(500, bst.Root.Value);
        }

        [TestMethod]
        public void TreeAdd7ElementsAssertItemsInRow()
        {
            var bst = new BinarySearchTree<int>();
            var collectionOfItems = new[] { 500, 250, 750, 125, 275, 625, 875 };

            foreach (var collectionOfItem in collectionOfItems)
            {
                bst.Add(collectionOfItem);
            }

            using (var enumerator = bst.GetEnumerator())
            {

                foreach (var t in collectionOfItems)
                {
                    enumerator.MoveNext();
                    var item = enumerator.Current;
                    Assert.AreEqual(t, item);

                }
            }
        }

        [TestMethod]
        public void Add7ItemsSearchForExistingItemAssertContainsTrue()
        {
            var bst = new BinarySearchTree<int>();
            var collectionOfItems = new[] { 500, 250, 750, 125, 275, 625, 875 };
            foreach (var collectionOfItem in collectionOfItems)
            {
                bst.Add(collectionOfItem);
            }

            Assert.IsTrue(bst.Contains(275));

        }

        [TestMethod]
        public void Add7ItemsSearchForNonExistingItemAssertContainsFalse()
        {
            var bst = new BinarySearchTree<int>();
            var collectionOfItems = new[] { 500, 250, 750, 125, 275, 625, 875 };
            foreach (var collectionOfItem in collectionOfItems)
            {
                bst.Add(collectionOfItem);
            }

            Assert.IsFalse(bst.Contains(900));
        }

        [TestMethod]
        public void AddOneItemAndRemoveAssertCountZeroHeadNull()
        {
            var bst = new BinarySearchTree<int>();
            bst.Add(500);

            bst.Remove(500);

            Assert.AreEqual(0, bst.Count);
            Assert.IsNull(bst.Root);
        }

        [TestMethod]
        public void AddThreeItemsRemoveTailOnLeftAssertLeftNullRightOk()
        {
            var bst = new BinarySearchTree<int>();
            bst.Add(500);
            bst.Add(250);
            bst.Add(750);

            bst.Remove(250);

            Assert.AreEqual(2, bst.Count);
            Assert.AreEqual(500, bst.Root.Value);
            Assert.AreEqual(750, bst.Root.Right.Value);
            Assert.IsNull(bst.Root.Left);
        }

        [TestMethod]
        public void AddThreeItemsRemoveTailOnRightAssertRightNullLeftOk()
        {
            var bst = new BinarySearchTree<int>();
            bst.Add(500);
            bst.Add(250);
            bst.Add(750);

            bst.Remove(750);

            Assert.AreEqual(2, bst.Count);
            Assert.AreEqual(500, bst.Root.Value);
            Assert.AreEqual(250, bst.Root.Left.Value);
            Assert.IsNull(bst.Root.Right);
        }

        [TestMethod]
        public void AddFiveItemsRemoveItemOnLeftOfHead()
        {
            var bst = new BinarySearchTree<int>();
            bst.Add(500);
            bst.Add(250);
            bst.Add(750);
            bst.Add(125);
            bst.Add(625);

            bst.Remove(250);

            Assert.AreEqual(4, bst.Count);
            Assert.AreEqual(500, bst.Root.Value);
            Assert.AreEqual(125, bst.Root.Left.Value);
            Assert.AreEqual(750, bst.Root.Right.Value);
            Assert.AreEqual(625, bst.Root.Right.Left.Value);

            Assert.IsNull(bst.Root.Left.Left);
            Assert.IsNull(bst.Root.Left.Right);
        }

        [TestMethod]
        public void AddFiveItemsRemoveItemOnRightOfHead()
        {
            var bst = new BinarySearchTree<int>();
            bst.Add(500);
            bst.Add(250);
            bst.Add(750);
            bst.Add(125);
            bst.Add(625);

            bst.Remove(750);

            Assert.AreEqual(4, bst.Count);
            Assert.AreEqual(500, bst.Root.Value);
            Assert.AreEqual(250, bst.Root.Left.Value);
            Assert.AreEqual(125, bst.Root.Left.Left.Value);

            Assert.AreEqual(625, bst.Root.Right.Value);


            Assert.IsNull(bst.Root.Right.Left);
            Assert.IsNull(bst.Root.Right.Right);
        }

        [TestMethod]
        public void TestItemToRemoveHasRightWhichHasNoLeftChild_ItemToRemove_Left_Of_Parent()
        {
            var bst = new BinarySearchTree<int>();
            var collectionOfItems = new[] { 500, 250, 750, 125, 275, 625, 875, 300, 1000 };
            foreach (var collectionOfItem in collectionOfItems)
            {
                bst.Add(collectionOfItem);
            }

            bst.Remove(250);

            Assert.AreEqual(8, bst.Count);
            Assert.AreEqual(500, bst.Root.Value);
            Assert.AreEqual(275, bst.Root.Left.Value);
            Assert.AreEqual(125, bst.Root.Left.Left.Value);
            Assert.AreEqual(300, bst.Root.Left.Right.Value);

            Assert.AreEqual(750, bst.Root.Right.Value);
            Assert.AreEqual(625, bst.Root.Right.Left.Value);
            Assert.AreEqual(875, bst.Root.Right.Right.Value);
            Assert.AreEqual(1000, bst.Root.Right.Right.Right.Value);


            Assert.IsNull(bst.Root.Left.Right.Left);

        }

        [TestMethod]
        public void TestItemToRemoveHasRightWhichHasNoLeftChild_ItemToRemove_Right_Of_Parent()
        {
            var bst = new BinarySearchTree<int>();
            var collectionOfItems = new[] { 500, 250, 750, 125, 275, 625, 875, 300, 1000 };
            foreach (var collectionOfItem in collectionOfItems)
            {
                bst.Add(collectionOfItem);
            }

            bst.Remove(750);

            Assert.AreEqual(8, bst.Count);
            Assert.AreEqual(500, bst.Root.Value);
            Assert.AreEqual(250, bst.Root.Left.Value);
            Assert.AreEqual(125, bst.Root.Left.Left.Value);
            Assert.AreEqual(275, bst.Root.Left.Right.Value);
            Assert.AreEqual(300, bst.Root.Left.Right.Right.Value);

            Assert.AreEqual(875, bst.Root.Right.Value);
            Assert.AreEqual(625, bst.Root.Right.Left.Value);

            Assert.AreEqual(1000, bst.Root.Right.Right.Value);

        }

        [TestMethod]
        public void TestItemToRemoveHasRightWhichHasLeftChild_Removing_Head()
        {
            var bst = new BinarySearchTree<int>();
            var collectionOfItems = new[] { 500, 250, 750, 125, 275, 625, 875, 300, 1000 };
            foreach (var collectionOfItem in collectionOfItems)
            {
                bst.Add(collectionOfItem);
            }

            bst.Remove(500);

            Assert.AreEqual(8, bst.Count);
            Assert.AreEqual(625, bst.Root.Value);

            Assert.AreEqual(250, bst.Root.Left.Value);
            Assert.AreEqual(750, bst.Root.Right.Value);
            Assert.IsNull(bst.Root.Right.Left);
        }
    }
}

