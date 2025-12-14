using System.Collections;
using UnityEngine;

public class DraggableWeight : Interactable, ILeftClick, ILeftRelease
{
    private bool isDragging;
    private ISnappable snappedIn;
    [SerializeField] private float radius;
    [SerializeField] public int weight;
    
    void Start()
    {
        isDragging = false;
    }

    public void DoLeftClick()
    {
        if (snappedIn != null)
        {
            snappedIn.OnUnsnap(this);
            snappedIn = null;
        }

        isDragging = true;
        StartCoroutine(DoDrag());
    }

    public void DoLeftRelease()
    {
        isDragging = false;

        ISnappable snappableFound = FindSnappable();
        if (snappableFound != null)
        {
            snappedIn = snappableFound;
            snappableFound.OnSnap(this);
        }
    }

    private ISnappable FindSnappable()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            ISnappable snappable = hitCollider.GetComponent<ISnappable>();
            if (snappable != null)
            {
                return snappable;
            }
        }

        return null;
    }

    private IEnumerator DoDrag()
    {
        while (isDragging)
        {
            Vector3 dragTo = Camera.main.ScreenToWorldPoint(ClickCore.self.GetMousePosition());
            dragTo.z = 0;
            transform.position = dragTo;

            yield return null;
        }
    }
}
