using UnityEngine;

public class RockSlot : MonoBehaviour, ISnappable
{
    [SerializeField] int side = 0;
    private DraggableWeight snappedWeight;
    public void OnSnap(DraggableWeight draggable)
    {
        if (snappedWeight != null) return;

        snappedWeight = draggable;
        draggable.transform.position = transform.position;
        WinManager.self.AddWeight(side, snappedWeight.weight);

        FireSpawner.self.SpawnFires(side, WinManager.self.GetNumFiresLit(side));
            
        if (!WinManager.self.CheckForWin()) return;
        WinManager.self.TriggerWin();
    }

    public void OnUnsnap(DraggableWeight draggable)
    {
        WinManager.self.AddWeight(side, snappedWeight.weight * -1);
        snappedWeight = null;

        FireSpawner.self.SpawnFires(side, WinManager.self.GetNumFiresLit(side));
    }
}