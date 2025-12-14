using UnityEngine;

public class RockSlot : MonoBehaviour, ISnappable
{
    [SerializeField] int side = 0;
    private DraggableWeight snappedWeight;
    public void OnSnap(DraggableWeight draggable)
    {
        snappedWeight = draggable;
        draggable.transform.position = transform.position;
        WinManager.self.AddWeight(side, snappedWeight.weight);

        FireSpawner.self.SpawnFires(side, WinManager.self.GetNumFiresLit(side));
            
        if (!WinManager.self.CheckForWin()) return;
        //win stuff
    }

    public void OnUnsnap()
    {
        WinManager.self.AddWeight(side, snappedWeight.weight * -1);
        snappedWeight = null;
    }
}