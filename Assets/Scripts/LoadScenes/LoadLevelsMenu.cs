using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevelsMenu : MonoBehaviour {

    public InputField userName;
    public InputField userPassword;
    string username;
    string password;

    private void Start()
    {
        username = null;
        password = null;
    }

    // Update is called once per frame
    public void LoadLevelMenu()
    {

        if (userName.text != null && userPassword.text != null)
        {
            SceneManager.LoadScene(1);
        }
    }
    /*
        public void Quit()
        {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
        }
        */
}
