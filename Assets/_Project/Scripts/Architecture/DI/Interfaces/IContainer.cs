namespace Architecture.DI.Containers
{
    internal interface IContainer
    {
        IScope CreateScope();
    }
}
