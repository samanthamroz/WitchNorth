using UnityEngine;

public class RockCornerSlot : MonoBehaviour, ISnappable
{
    [SerializeField] int side1 = 0;
    [SerializeField] int side2 = 0;
    private DraggableWeight snappedWeight;
    public void OnSnap(DraggableWeight draggable)
    {
        if (snappedWeight != null) return;

        snappedWeight = draggable;
        draggable.transform.position = transform.position;
        WinManager.self.AddWeight(side1, snappedWeight.weight);
        WinManager.self.AddWeight(side2, snappedWeight.weight);

        FireSpawner.self.SpawnFires(side1, WinManager.self.GetNumFiresLit(side1));
        FireSpawner.self.SpawnFires(side2, WinManager.self.GetNumFiresLit(side2));
            
        if (!WinManager.self.CheckForWin()) return;
        WinManager.self.TriggerWin();
    }

    public void OnUnsnap(DraggableWeight draggable)
    {
        WinManager.self.AddWeight(side1, snappedWeight.weight * -1);
        WinManager.self.AddWeight(side2, snappedWeight.weight * -1);
        snappedWeight = null;

        FireSpawner.self.SpawnFires(side1, WinManager.self.GetNumFiresLit(side1));
        FireSpawner.self.SpawnFires(side2, WinManager.self.GetNumFiresLit(side2));
    }
}