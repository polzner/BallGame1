using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        Game.Load();
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
