using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    public static Navigator navi;
    public string currentNode;

    private void Start()
    {
        #region Singleton
        if (navi == null)
        {
            navi = this;
            DontDestroyOnLoad(this);
        } else
        {
            Destroy(gameObject);
        }
        #endregion
        currentNode = "MainMenu";
    }

    //This is for game objects that include the main menu, options menu, and the gameplay.
    public void Navigate(string from, string to)
    {

        GameObject previous = GameObject.Find(from);
        GameObject[] next = Resources.FindObjectsOfTypeAll<GameObject>();

        //Check if from or to was not defined. If they are, then navigate between them
        if (previous == null)
        {
            Debug.LogError("The parameter \"from\" is null.");
        }
        //Iterate through all the list to find the next object.  Once you find it, activate it
        for (int i = 0; i < next.Length; i++)
        {
            if (next[i].name == to)
            {
                next[i].SetActive(true);
                previous.SetActive(false);
                currentNode = next[i].name;
            }

        }
    }
}
