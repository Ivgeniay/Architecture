namespace Architecture
{
    public abstract class InteractorBase : IInteractor
    {
        protected RepositoryBase repository;
        public virtual void InitializeRepository()
        {
            if (repository != null)
                repository.Initialization();
        }

        public virtual void StartRepository()
        {
            if (repository != null)
                repository.Start();
        }

        public abstract void Start();
        public abstract void Initialization();

    }
}
