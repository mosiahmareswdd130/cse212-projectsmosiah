using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue three items with different priorities: Bob (1), Tim (3), Sue (2).
    //           Dequeue once.
    // Expected Result: Tim is returned because he has the highest priority (3).
    // Defect(s) Found: 
    public void TestPriorityQueue_HighestPriorityDequeued()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Bob", 1);
        priorityQueue.Enqueue("Tim", 3);
        priorityQueue.Enqueue("Sue", 2);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("Tim", result);
    }

    [TestMethod]
    // Scenario: Enqueue three items with the same priority: Alpha (2), Beta (2), Gamma (2).
    //           Dequeue once.
    // Expected Result: Alpha is returned because it was added first (FIFO tiebreaker).
    // Defect(s) Found: 
    public void TestPriorityQueue_TiePriorityUsesFIFO()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Alpha", 2);
        priorityQueue.Enqueue("Beta", 2);
        priorityQueue.Enqueue("Gamma", 2);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("Alpha", result);
    }

    [TestMethod]
    // Scenario: Enqueue Low (1), High (5), Mid (3). Dequeue all three.
    // Expected Result: High, Mid, Low — dequeued in descending priority order.
    // Defect(s) Found: 
    public void TestPriorityQueue_FullDequeueOrder()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("High", 5);
        priorityQueue.Enqueue("Mid", 3);

        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Mid", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue A (2), B (5), C (5), D (1). Dequeue all four.
    // Expected Result: B, C, A, D — B before C because B was enqueued first (FIFO tiebreaker).
    // Defect(s) Found: 
    public void TestPriorityQueue_TiesInMiddleUseFIFO()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 2);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 5);
        priorityQueue.Enqueue("D", 1);

        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("D", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty queue.
    // Expected Result: InvalidOperationException thrown with message "The queue is empty."
    // Defect(s) Found: 
    public void TestPriorityQueue_EmptyQueueThrowsException()
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