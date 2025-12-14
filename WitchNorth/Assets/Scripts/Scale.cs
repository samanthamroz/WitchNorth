using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    [SerializeField] ScaleSlot slot1, slot2;

    Dictionary<ScaleSlot, int> slotWeights;
    [SerializeField] Sprite balanced, leftHeavy, rightHeavy;
    public void Start()
    {
        slotWeights = new Dictionary<ScaleSlot, int>{{ slot1, 0 },{ slot2, 0 }};
        GetComponent<SpriteRenderer>().sprite = balanced;
    }

    public void AddWeight(ScaleSlot slot, int weight)
    {
        slotWeights[slot] += weight;
        UpdateGraphic();
    }

    private void UpdateGraphic()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (slotWeights[slot1] == slotWeights[slot2])
        {
            renderer.sprite = balanced;
        } else if (slotWeights[slot1] > slotWeights[slot2])
        {
            renderer.sprite = leftHeavy;
        } else
        {
            renderer.sprite = rightHeavy;
        }
    }
}