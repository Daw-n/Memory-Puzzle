using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class afterclicklevel1 : MonoBehaviour
{
    public void sceneselect(int level)
    {
        Application.LoadLevel(level);
    }
    public void quitgame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

}
