using UnityEngine;


internal class PlayerBehaviour : MonoBehaviour
{

    private PlayerController playerController;

    private void Awake()
    {
        Engine.Instance.OnSceneStartEvent += OnSceneStart;
    }

    private void OnSceneStart()
    {
        playerController = Engine.Instance.GetController<PlayerController>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            playerController.AddCoins(this, 5);
            Debug.Log(playerController.GetNumCoins());
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerController.SpendCoins(this, 5);
            Debug.Log(playerController.GetNumCoins());
        }
    }
    
}
