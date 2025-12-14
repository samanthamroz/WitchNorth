using System.Collections.Generic;
using UnityEngine;

public class FireSpawner : MonoBehaviour
{
    public static FireSpawner self;
    [SerializeField] private GameObject fireSprite;
    [SerializeField] private List<GameObject> fireHolders;
    void Awake()
    {
        if (self == null) self = this;
    }

    public void SpawnFires(int side, int numFires)
    {
        GameObject holder = fireHolders[side];
        DestroyChildren(holder);

        for (int i = 0; i < numFires; i++)
        {
            GameObject fire = Instantiate(fireSprite, holder.transform);
            fire.transform.Translate(new(i, 0, 0));
        }
    }

    private void DestroyChildren(GameObject gameObject)
    {
        for (int i = gameObject.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
    }
}