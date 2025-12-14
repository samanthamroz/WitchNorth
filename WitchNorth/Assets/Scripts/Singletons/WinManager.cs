using System.Collections.Generic;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    public static WinManager self;
    [SerializeField] private int numSides = 3;
    [SerializeField] private int correctTotal = 20;
    private int[] totals;
    private bool onLevel1 = true;


    void Awake()
    {
        if (self == null) self = this;
    }

    void Start()
    {
        totals = new int[numSides];
    }

    public void AddWeight(int side, int amount)
    {
        totals[side] += amount;
    }

    public int GetNumFiresLit(int side)
    {
        int total = totals[side];
        
        if (total == 0) return 0;
        
        float ratio = (float)total / correctTotal;
        
        if (ratio == 1f) return 3;
        if (ratio < 0.5f) return 1;
        if (ratio < 1f) return 2;
        if (ratio <= 1.25f) return 4;
        return 5;
    }

    public bool CheckForWin()
    {
        foreach(int i in totals)
        {
            if (i != correctTotal) return false;
        }
        return true;
    }

    public void TriggerWin()
    {
        if (onLevel1)
        {
            NarrativeManager.self.Narrative1();
            numSides = 4;
            totals = new int[numSides];
            correctTotal = 26;
            onLevel1 = false;
        } else
        {
            NarrativeManager.self.Narrative2();
        }
    }
}