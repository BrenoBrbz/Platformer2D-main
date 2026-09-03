using UnityEngine;

public class Quit : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Saiu do jogo!"); //Para saber se funciona
    }
}

