/// <summary>
/// A basic implementation of a Queue
/// </summary>
public class PersonQueue
{
    private readonly List<Person> _queue = new();

    public int Length => _queue.Count;

    // <param name="person">The person to add</param>
    public void Enqueue(Person person)
    {
        //_queue.Insert(0, person);
        // Needed to change this line to make this function properly add to the back of the list
        _queue.Add(person);
    }

    public Person Dequeue()
    {
        // This line of code throws an error if the queue is empty
        if (IsEmpty())
            throw new InvalidOperationException("Queue is empty.");

        var person = _queue[0];
        _queue.RemoveAt(0);
        return person;
    }

    public bool IsEmpty()
    {
        return Length == 0;
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _queue)}]";
    }
}