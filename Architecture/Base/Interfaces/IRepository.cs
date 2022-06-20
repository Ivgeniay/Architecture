using System.Collections;

namespace Architecture
{
    public interface IRepository
    {
        abstract IEnumerator InitializeRepository();
        abstract IEnumerator Start();
        abstract IEnumerator Save();

    }
}
