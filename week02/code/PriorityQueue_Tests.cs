using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue three items with distinct priorities (low, medium, high) in that order,
    // then dequeue. Requirement: Dequeue must remove and return the item with the highest priority.
    // Expected Result: The high-priority item ("high") is returned.
    // Defect(s) Found: The for-loop condition was "index < _queue.Count - 1", which skipped
    // checking the last item in the list, so the true highest-priority item could be missed if
    // it was last in the queue.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("low", 1);
        priorityQueue.Enqueue("medium", 3);
        priorityQueue.Enqueue("high", 5);

        var value = priorityQueue.Dequeue();

        Assert.AreEqual("high", value);
    }

    [TestMethod]
    // Scenario: Enqueue two items that share the same highest priority ("first" then "second",
    // both priority 5), plus a lower priority item. Requirement: when priorities tie, the item
    // closest to the front of the queue (added first) must be dequeued first.
    // Expected Result: "first" is returned, not "second".
    // Defect(s) Found: The comparison used ">=" instead of ">", which caused later items with an
    // equal priority to overwrite the recorded highPriorityIndex, so the LAST tied item was
    // returned instead of the first (violates the FIFO tie-break requirement).
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("first", 5);
        priorityQueue.Enqueue("low", 2);
        priorityQueue.Enqueue("second", 5);

        var value = priorityQueue.Dequeue();

        Assert.AreEqual("first", value);
    }

    [TestMethod]
    // Scenario: Enqueue several items and dequeue repeatedly until the queue is empty.
    // Requirement: Dequeue must remove the item from the queue, not just read it, so previously
    // dequeued items are never returned again and the queue actually shrinks.
    // Expected Result: Items come back highest priority first (with FIFO tie-break), each exactly
    // once, in this order: "b", "a", "d", "c".
    // Defect(s) Found: Dequeue never called _queue.RemoveAt(...), so the "removed" item stayed in
    // the queue and could be returned again on a later call.
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("a", 3);
        priorityQueue.Enqueue("b", 4);
        priorityQueue.Enqueue("c", 1);
        priorityQueue.Enqueue("d", 2);

        Assert.AreEqual("b", priorityQueue.Dequeue());
        Assert.AreEqual("a", priorityQueue.Dequeue());
        Assert.AreEqual("d", priorityQueue.Dequeue());
        Assert.AreEqual("c", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Call Dequeue on a brand-new, empty PriorityQueue.
    // Requirement: If the queue is empty, an InvalidOperationException must be thrown with the
    // message "The queue is empty."
    // Expected Result: InvalidOperationException is thrown with that exact message.
    // Defect(s) Found: None. This case already worked correctly before any fixes were applied.
    public void TestPriorityQueue_4()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                 string.Format("Unexpected exception of type {0} caught: {1}",
                                e.GetType(), e.Message)
            );
        }
    }
}
