namespace OopLab.Domain;

public abstract class Person
{
    public string FirstName { get; }
    public string LastName { get; }

    protected Person(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public abstract string GetDescription();
}
