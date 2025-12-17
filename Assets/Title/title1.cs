using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class title1 : MonoBehaviour
{
    public void SwitchScene()
    {
        SceneManager.LoadScene("Title2Scene",LoadSceneMode.Single);
    }
}