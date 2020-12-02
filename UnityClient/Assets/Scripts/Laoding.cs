using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laoding : MonoBehaviour
{
    void Start()
    {
        if(DataManager.SceneToLoad != null)
        {
            try
            {
                SceneManager.LoadSceneAsync(DataManager.SceneToLoad);
                DataManager.SceneToLoad = "";
            }
            catch
            {
                Debug.Log("Scene cannot load");
            }
        }


    }

}
