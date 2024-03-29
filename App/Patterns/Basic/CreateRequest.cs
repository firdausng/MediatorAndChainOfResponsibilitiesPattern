namespace App.Patterns.Basic;
public class CreateRequest : IRequest
{
    public CreateRequest(string name)
    {
        Name = name;
    }

    public string Name { get; }
}