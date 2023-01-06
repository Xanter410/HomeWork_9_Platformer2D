using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame(int SceneIndex)
    {
        SceneTransition.SwitchToScene(SceneIndex);
    }
}
