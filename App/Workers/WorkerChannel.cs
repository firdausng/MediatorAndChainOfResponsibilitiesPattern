using System.Threading.Channels;
using App.Patterns.Workers;

namespace App.Workers;

public class WorkerChannel
{
    private readonly Channel<(PublishType publishType, WorkerRequest workerRequest)> _channel =
        Channel.CreateUnbounded<(PublishType, WorkerRequest)>();

    public ChannelReader<(PublishType publishType, WorkerRequest workerRequest)> Reader => _channel.Reader;
    public ChannelWriter<(PublishType publishType, WorkerRequest workerRequest)> Writer => _channel.Writer;
}