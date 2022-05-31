using H.Pipes;
using Shared;

Console.WriteLine("Client starting.");

async Task ClientOperation(int index)
{
    Console.WriteLine("In ClientOperation(" + index + ")");

    TaskCompletionSource<Message> tcs = new TaskCompletionSource<Message>();
    await using var client = new PipeClient<Message>("PIPE_NAME");

    client.MessageReceived += (o, args) =>
    {
        tcs.SetResult(args.Message!);
        Console.WriteLine("MessageReceived: " + args.Message);
    };
    client.Disconnected += (o, args) => Console.WriteLine("Disconnected from server");
    client.Connected += (o, args) => Console.WriteLine("Connected to server");
    //client.ExceptionOccurred += (o, args) => OnExceptionOccurred(args.Exception);

    Console.WriteLine("Connecting to pipe " + index);

    await client.ConnectAsync();

    Console.WriteLine("Connected!");

    await client.WriteAsync(new Message
    {
        Request = "Hello " + index + "!",
    });

    var response = await tcs.Task;

    Console.WriteLine(response);
}

// Sequential version
for (int i = 0; i < 1000; i++)
{
    await ClientOperation(i);
}

/*// Parallel version
for (int i = 0; i < 10; i++)
{
    var tasks = Enumerable.Range(0, 10).Select(j => ClientOperation(j));
    await Task.WhenAll(tasks);
}*/
