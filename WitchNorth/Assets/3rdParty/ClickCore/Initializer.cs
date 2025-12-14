using UnityEngine;

public class Initializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnRuntimeMethodLoad() //This gets called automatically by Unity
    {
        GameObject prefab = Resources.Load<GameObject>("~ClickCore");
        GameObject obj = GameObject.Instantiate(prefab);
        obj.name = "~ClickCore";
    }
}