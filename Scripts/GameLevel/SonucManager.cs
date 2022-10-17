using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SonucManager : MonoBehaviour
{
    public void OyunaYenidenBasla()
    {
        SceneManager.LoadScene("Oyunici");
    }
    public void AnaMenuyeDon()
    {
        SceneManager.LoadScene("Menü");
    }
}
