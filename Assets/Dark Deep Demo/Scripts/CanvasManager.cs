using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DarkDeepDemo
{
    public class CanvasManager : MonoBehaviour
    {
        public void openLink(string link)
        {
            Application.OpenURL(link);
        }

    }
}
