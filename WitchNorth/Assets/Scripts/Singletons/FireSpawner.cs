using System.Collections.Generic;
using UnityEngine;

public class FireSpawner : MonoBehaviour
{
    public static FireSpawner self;
    [SerializeField] private GameObject fireSprite;
    [SerializeField] public List<GameObject> fireHolders;
    void Awake()
    {
        if (self == null) self = this;
    }

    public void SpawnFires(int side, int numFires)
    {
        GameObject holder = fireHolders[side];
        DestroyChildren(holder);

        List<GameObject> fires = new();
        for (int i = 0; i < numFires; i++)
        {
            GameObject fire = Instantiate(fireSprite, holder.transform);
            fires.Add(fire);
        }
        Center(fires);
    }

    private void DestroyChildren(GameObject gameObject)
    {
        for (int i = gameObject.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
    }

    private void Center(List<GameObject> fires)
    {
        float startingX = (1 - fires.Count) / 2f;
        for (int i = 0; i < fires.Count; i++)
        {
            fires[i].transform.localPosition = transform.localPosition + new Vector3(startingX + i, 0, 0);
            fires[i].transform.rotation = Quaternion.identity;
        }
    }
}