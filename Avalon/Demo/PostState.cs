namespace Demo;

public class PostState : State
{
    public override bool Execute()
    {
        ThreadCurrent current;
        current = new ThreadCurrent();
        current.Init();

        ThreadThread thread;
        thread = current.Thread;
        thread.ExitEventLoop(0x89f6);

        Console console;
        console = Console.This;
        console.Out.Write("PostState.Execute Aaa\n");
        return true;
    }
}