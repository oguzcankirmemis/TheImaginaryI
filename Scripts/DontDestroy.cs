using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour {

    private static DontDestroy instance = null;

    public static DontDestroy Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            SceneManager.LoadScene(1);
        else if (Input.GetKeyDown(KeyCode.F2))
            SceneManager.LoadScene(2);
        else if (Input.GetKeyDown(KeyCode.F3))
            SceneManager.LoadScene(3);
        else if (Input.GetKeyDown(KeyCode.F4))
            SceneManager.LoadScene(4);
        else if (Input.GetKeyDown(KeyCode.F5))
            SceneManager.LoadScene(5);
        else if (Input.GetKeyDown(KeyCode.F6))
            SceneManager.LoadScene(6);
        else if (Input.GetKeyDown(KeyCode.F7))
            SceneManager.LoadScene(7);
        else if (Input.GetKeyDown(KeyCode.F8))
            SceneManager.LoadScene(8);
        else if (Input.GetKeyDown(KeyCode.F11))
            SceneManager.LoadScene(0);
        else if (Input.GetKeyDown(KeyCode.F12))
            SceneManager.LoadScene(9);

    }
}
