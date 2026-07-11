/// <summary>
/// This queue is circular.  When people are added via AddPerson, then they are added to the 
/// back of the queue (per FIFO rules).  When GetNextPerson is called, the next person
/// in the queue is saved to be returned and then they are placed back into the back of the queue.  Thus,
/// each person stays in the queue and is given turns.  When a person is added to the queue, 
/// a turns parameter is provided to identify how many turns they will be given.  If the turns is 0 or
/// less than they will stay in the queue forever.  If a person is out of turns then they will 
/// not be added back into the queue.

public class TakingTurnsQueue
{
    private readonly Queue<Person> _queue = new Queue<Person>();

    /// <summary>
    /// Returns how many people are currently in the queue.
    /// </summary>
    public int Length => _queue.Count;

    /// <summary>
    /// Adds a new person to the back of the queue.
    /// </summary>
    public void AddPerson(string name, int turns)
    {
        _queue.Enqueue(new Person(name, turns));
    }

    /// <summary>
    /// Removes the front person from the queue, returns them,
    /// and re-enqueues them if they still have turns remaining.
    /// </summary>
    public Person GetNextPerson()
    {
        if (_queue.Count == 0)
            throw new InvalidOperationException("No one in the queue.");

        Person person = _queue.Dequeue();

        if (person.HasInfiniteTurns())
        {
            // Infinite turns: re-enqueue the exact same person unchanged
            // This is critical — the test verifies Turns is never modified
            _queue.Enqueue(person);
        }
        else
        {
            // Finite turns: decrement and re-enqueue only if turns remain
            Person withLessTurns = person.WithOneLessTurn();
            if (withLessTurns.Turns > 0)
                _queue.Enqueue(withLessTurns);
        }

        return person;
    }
}