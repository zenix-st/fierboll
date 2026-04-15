using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public  int levelIndex;
    public void nextlevel()
    {
        SceneManager.LoadScene(levelIndex);
    } 
    public void Quitapp()
    {
        Application.Quit();
    }



}