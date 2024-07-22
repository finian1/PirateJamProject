using UnityEngine;
using System.Collections;

public class CustomCoroutine : MonoBehaviour
{
    private static CustomCoroutine _instance;

    public static CustomCoroutine Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("CustomCoroutine");
                _instance = obj.AddComponent<CustomCoroutine>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    public void RunCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}