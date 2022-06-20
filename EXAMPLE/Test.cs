using UnityEngine;
using Architecture;
using Random = UnityEngine.Random;

public class Test : MonoBehaviour
{
    private PlayerInteractor player;

    void Awake()
    {
        App.Initialize();
        player = App.GetInteractor<PlayerInteractor>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            var t = Random.Range(0, 6);
            player.TakeCure(t);
            Debug.Log(player.GetHealth().ToString());
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            var t = Random.Range(0, 6);
            player.TakeDamage(t);
            Debug.Log(player.GetHealth().ToString());
        }
    }
}