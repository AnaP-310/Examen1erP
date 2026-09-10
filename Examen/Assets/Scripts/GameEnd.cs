using UnityEngine;

public class GameEnd : MonoBehaviour
{
    public GameOverMenu gameOverMenu;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pj"))
        {
            gameOverMenu.ShowGameOver();
        }
    }
}