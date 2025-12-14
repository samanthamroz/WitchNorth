using System.Collections.Generic;
using UnityEngine;

public class ScaleSlot : MonoBehaviour, ISnappable
{
    [SerializeField] Scale parentScale;
    List<DraggableWeight> snappedWeights = new();
    Vector3 originalPosition;
    void Start()
    {
        originalPosition = transform.position;
    }
    public void OnSnap(DraggableWeight draggable)
    {
        if (snappedWeights.Count >= 3) return;

        snappedWeights.Add(draggable);
        
        Center();

        parentScale.AddWeight(this, draggable.weight);
    }

    public void OnUnsnap(DraggableWeight draggable)
    {
        parentScale.AddWeight(this, draggable.weight * -1);
        snappedWeights.Remove(draggable);

        Center();
    }

    private void Center()
    {
        float startingX = (1 - snappedWeights.Count) / 2f;
        for (int i = 0; i < snappedWeights.Count; i++)
        {
            snappedWeights[i].transform.position = transform.position + new Vector3(startingX + i, 0, 0);
        }
    }
}