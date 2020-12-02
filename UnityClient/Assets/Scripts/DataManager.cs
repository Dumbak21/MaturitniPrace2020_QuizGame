using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class DataManager
{
    public static string NickName;


    #region SCENES
    public static string SceneToLoad;
    public static List<string> LastLoaded = new List<string>();

    public static void LoadScene(string scene)
    {
        SceneToLoad = scene;
        LastLoaded.Add(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Loading");
    }

    public static void LoadPreviousScene()
    {
        //LastLoaded.ForEach(x => Debug.Log(x.ToString()));
        if (LastLoaded.Count > 0)
        {
            SceneToLoad = LastLoaded[LastLoaded.Count - 1];
            LastLoaded.RemoveAt(LastLoaded.Count - 1);
            SceneManager.LoadScene("Loading");
        }

    }
    #endregion


    #region SETTINGS

    public static float Volume = 70;
    public static int Quality = 1;
    //public static Resolution resolution;


    #endregion



}
public class Resolution
{
    public int[] res{ 
        get
        {
            return new int[]{ x, y };
        }
        set
        {
            x = value[0];
            y = value[1];
        }
    }

    private int x;
    private int y;
}