using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    public void EN_btn()
    {
        RPGCore.lang = RPGCore.Lang.EN;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene1");
    }
    public void CH_btn()
    {
        RPGCore.lang = RPGCore.Lang.ZH;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene1");
    }

    public void Back_btn(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
    }
}
