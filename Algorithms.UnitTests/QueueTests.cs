using Algorithms.StacksAndQueues;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithms.UnitTests
{
    [TestClass]
    public class QueueTests
    {
        [TestMethod]
        public void AssertCountZeroAtInitialization()
        {
            var queue = new Queue<int>();
            Assert.IsTrue(queue.Count == 0);
        }

        [TestMethod]
        public void AssertSizeFourAtEmptyInitialization()
        {
            var queue = new Queue<int>();
            Assert.IsTrue(queue.InitialSize == 4);
        }

        [TestMethod]
        public void AssertSizeSixteenWhenInstantiatedWithSixteenSize()
        {
            var queue = new Queue<int>(16);
            Assert.IsTrue(queue.InitialSize == 16);
        }

        [TestMethod]
        public void AddFiveElementsAssertCountFive()
        {
            var queue = new Queue<int>();

            for (int i = 0; i < 5; i++)
            {
                queue.Enqueue(i);
            }

            Assert.AreEqual(5, queue.Count);
        }

        [TestMethod]
        public void AddFiveElementsAssertElementsAddedInOrder()
        {
            var queue = new Queue<int>();

            for (int i = 0; i < 5; i++)
            {
                queue.Enqueue(i);
            }

            for (int i = 0; i < 5; i++)
            {
                var item = queue.Dequeue();
                Assert.AreEqual(i, item);
            }
        }

        [TestMethod]
        public void AddSixteenItems_RemoveNineItems_AssertCorrectBehaviourOfResize()
        {
            var queue = new Queue<int>();

            for (int i = 0; i < 16; i++)
            {
                queue.Enqueue(i);
            }

            for (int k = 15; k > 6; k--)
            {
                var item = queue.Dequeue();
            }

            Assert.AreEqual(7, queue.Count);
        }

        [TestMethod]
        public void AddFour_RemoveTwo_AddFour_Assert_CorrectBehviour()
        {
            var queue = new Queue<int>();

            for (int i = 0; i < 4; i++)
            {
                queue.Enqueue(i);
            }

            for (int k = 0; k < 2; k++)
            {
                var item = queue.Dequeue();
                Assert.AreEqual(item, k);
            }

            for (int j = 5; j < 8; j++)
            {
                queue.Enqueue(j);
            }

            for (int k = 2; k < 4; k++)
            {
                var item = queue.Dequeue();
                Assert.AreEqual(item, k);
            }

        }

        [TestMethod]
        public void AddThree_RemoveTwo_AddOne_Assert_CorrectBehviourForWrappedHead()
        {
            var queue = new Queue<int>();

            for (int i = 0; i < 3; i++)
            {
                queue.Enqueue(i);
            }

            for (int k = 0; k < 2; k++)
            {
                var item = queue.Dequeue();
                Assert.AreEqual(item, k);
            }

            //Add two more
            for (int j = 3; j < 5; j++)
            {
                queue.Enqueue(j);
            }

            for (int i = 2; i < 5; i++)
            {
                var item = queue.Dequeue();
                Assert.AreEqual(item, i);
            }

        }
    }
}
