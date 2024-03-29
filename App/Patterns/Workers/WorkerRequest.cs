namespace App.Patterns.Workers;

public class WorkerRequest : IRequest
{
    public WorkerRequest(string name)
    {
        Name = name;
    }

    public string Name { get; }
}