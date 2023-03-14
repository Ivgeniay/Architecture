using Architecture.Pools.ObjectsPool;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private testElement testElement;
    [SerializeField] private Transform container;

    private MonoPool<testElement> monoPool;

    void Start() {
        monoPool = new MonoPool<testElement>(testElement, 25, null, true);
        monoPool.HasFreeElement(out testElement testE);

        var obj1 = monoPool.GetUnactiveObject();
        Debug.Log($"Unactive: " + obj1);

        var obj2 = monoPool.GetAllUnactiveObject();
        Debug.Log("All unactive:" + obj2.Count);

        var tt = monoPool.GetAllActiveObjects();
        Debug.Log("GetAllActive count"+ tt.Count);

        Debug.Log("Active:" + monoPool.GetCountActiveObjects());
        Debug.Log("Unactive:" + monoPool.GetCountUnactiveObjects());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
