/// <summary>
/// This queue is circular.  When people are added via AddPerson, then they are added to the 
/// back of the queue (per FIFO rules).  When GetNextPerson is called, the next person
/// in the queue is saved to be returned and then they are placed back into the back of the queue.  Thus,
/// each person stays in the queue and is given turns.  When a person is added to the queue, 
/// a turns parameter is provided to identify how many turns they will be given.  If the turns is 0 or
/// less than they will stay in the queue forever.  If a person is out of turns then they will 
/// not be added back into the queue.
/// </summary>
public class TakingTurnsQueue
{

    private readonly PersonQueue _people = new();

    public int Length => _people.Length;

    public void AddPerson(string name, int turns)
    {
        var person = new Person(name, turns);
        _people.Enqueue(person);
    }

    // /// <summary>
    // /// Get the next person in the queue and return them. The person should
    // /// go to the back of the queue again unless the turns variable shows that they 
    // /// have no more turns left.  Note that a turns value of 0 or less means the 
    // /// person has an infinite number of turns.  An error exception is thrown 
    // /// if the queue is empty.
    // /// </summary>
    public Person GetNextPerson()
    {

        if (_people.IsEmpty())
        {
            throw new InvalidOperationException("No one in the queue.");
        }

        var person = _people.Dequeue();

        // This if statement checks if a person's turn value is 0 or below, and if it is, they have unlimited turns, so put them back in the queue unchanged
        if (person.Turns <= 0)
        {
            _people.Enqueue(person);
        }
        // This else if statement checks if a person has more than 1 turn left, and if they do, it reduces their turn count and puts them back in the queue
        // if (person.Turns > 1)
        else if (person.Turns > 1)
        {
            person.Turns = person.Turns - 1;
            _people.Enqueue(person);
        }
        // If this was the person’s last turn, don’t add them back to the queue

        return person;
    }

    public override string ToString()
    {
        return _people.ToString();
    }
}