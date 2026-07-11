public class PriorityQueue
{
    private List<PriorityItem> _queue = new List<PriorityItem>();
 
    /// <summary>
    /// Adds a new item with a given priority to the back of the queue.
    /// </summary>
    public void Enqueue(string value, int priority)
    {
        _queue.Add(new PriorityItem(value, priority));
    }
 
    /// <summary>
    /// Removes and returns the value of the highest priority item.
    /// If there is a tie, the item closest to the front (FIFO) is removed.
    /// Throws InvalidOperationException if the queue is empty.
    /// </summary>
    public string Dequeue()
    {
        if (_queue.Count == 0)
            throw new InvalidOperationException("The queue is empty.");
 
        // Find the index of the highest priority item.
        // We iterate front to back so the first (earliest added) item
        // with the max priority is chosen naturally — satisfying FIFO on ties.
        int highestIndex = 0;
        for (int i = 1; i < _queue.Count; i++)
        {
            // Strictly greater than — NOT >= — so we keep the first occurrence
            // of the highest priority (FIFO tiebreaker).
            if (_queue[i].Priority > _queue[highestIndex].Priority)
            {
                highestIndex = i;
            }
        }
 
        string value = _queue[highestIndex].Value;
        _queue.RemoveAt(highestIndex);
        return value;
    }
 
    public override string ToString()
    {
        return $"[{string.Join(", ", _queue)}]";
    }
}
 
/// <summary>
/// Holds a single item in the priority queue: a value and its priority.
/// </summary>
internal class PriorityItem
{
    public string Value { get; private set; }
    public int Priority { get; private set; }
 
    public PriorityItem(string value, int priority)
    {
        Value = value;
        Priority = priority;
    }
 
    public override string ToString() => $"{Value} (Pri:{Priority})";
}