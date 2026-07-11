/// <summary>
/// Represents a person in the taking turns queue.
/// Turns <= 0 means infinite turns (person is never removed from the queue).
/// Turns > 0 means a finite number of turns remaining.
/// </summary>
public class Person
{
    public string Name { get; private set; }
    public int Turns { get; private set; }

    public Person(string name, int turns)
    {
        Name = name;
        Turns = turns;
    }

    /// <summary>
    /// Returns true if this person has infinite turns (turns value is 0 or less).
    /// </summary>
    public bool HasInfiniteTurns() => Turns <= 0;

    /// <summary>
    /// Returns a new Person with one fewer turn remaining.
    /// Only called for finite-turn people.
    /// We create a new Person rather than modifying this one to keep
    /// the original Turns value untouched (important for infinite-turn checks).
    /// </summary>
    public Person WithOneLessTurn() => new Person(Name, Turns - 1);
}