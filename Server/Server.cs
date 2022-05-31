using H.Pipes;
using Shared;

Console.WriteLine("Server starting.");

await using var server = new PipeServer<Message>("PIPE_NAME");

server.ClientConnected += (o, args) =>
{
    Console.WriteLine($"Client {args.Connection.PipeName} is now connected!");
};
server.ClientDisconnected += (o, args) =>
{
    Console.WriteLine($"Client {args.Connection.PipeName} disconnected");
};
server.MessageReceived += async (sender, args) =>
{
    Console.WriteLine($"Client {args.Connection.PipeName} says: {args.Message}");
    var response = new Message { Response = args.Message!.Request + args.Message!.Request };
    Console.WriteLine($"Sending to {args.Connection.PipeName}: {response}");
    await args.Connection.WriteAsync(response);
};

server.ExceptionOccurred += (object? sender, H.Pipes.Args.ExceptionEventArgs e) =>
{
    Console.WriteLine("Server is CRASHING!!!!!!!!!!!!!!!!!!!!!!!!");
    Console.WriteLine(e);
};

await server.StartAsync();

Console.WriteLine("Server is ready!");


await Task.Delay(Timeout.InfiniteTimeSpan);
