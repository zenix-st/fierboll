using UnityEngine;

public class startscene : MonoBehaviour
{
    public GameObject win;
    public GameObject me;

    public void winee()
    {
        win.SetActive(true);
        me.SetActive(false);
    }
}
