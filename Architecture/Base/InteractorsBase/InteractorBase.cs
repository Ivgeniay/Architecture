namespace Architecture
{
    public abstract class InteractorBase : IInteractor
    {
        protected RepositoryBase repository;
        public virtual void InitializeRepository()
        {
            repository.Initialization();
        }

        public virtual void StartRepository()
        {
            repository.Start();
        }

        public virtual void Start()
        {

        }

        public virtual void Initialization()
        {

        }

    }
}
