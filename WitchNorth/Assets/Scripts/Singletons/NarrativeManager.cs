using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NarrativeManager : Interactable, ILeftClick 
{
    public static NarrativeManager self;
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject textPanel, winPanel, hint1, hint2, blocks1, blocks2, slots1, slots2, compass, fireHolders1;
    [SerializeField] List<GameObject> fireHolders2;
    [SerializeField] Scale scale;
    private int narrativeState = 0;
    private int advanceState = 0;

    public void DoLeftClick()
    {
        switch (narrativeState)
        {
            case 0:
                Narrative0();
                break;
            case 1:
                Narrative1();
                break;
            case 2:
                Narrative2();
                break;
            default:
                break;
        }
        advanceState++;
    }

    void Awake()
    {
        if (self == null) self = this;
    }

    void Start()
    {
        
    }

    void Narrative0()
    {
        switch (advanceState)
        {
            case 0:
                text.text = "I was looking for my keys.";
                break;
            case 1:
                text.text = "I know I am somewhere South of my house, but I don’t know which way North is.";
                break;
            case 2:
                text.text = "The good news is I have some magic rocks that can summon what I need...";
                break;
            case 3:
                text.text = "The bad news is I only halfway know how to use them.";
                break;
            case 4:
                text.text = "They only came with a scale and a scrap of paper, no instructions.";
                break;
            case 5:
                text.text = "If I can figure this out, maybe I can get out of here.";
                break;
            case 6:
                textPanel.SetActive(false);
                GetComponent<Collider>().enabled = false;
                narrativeState++;
                advanceState = -1;
                break;
        }
    }

    public void Narrative1()
    {
        switch (advanceState)
        {
            case 0:
                blocks2.SetActive(true);
                blocks1.SetActive(false);
                hint1.SetActive(false);
                hint2.SetActive(true);
                slots1.SetActive(false);
                slots2.SetActive(true);
                fireHolders1.SetActive(false);
                FireSpawner.self.fireHolders = fireHolders2;
                scale.Start();
                text.text = "It summoned more rocks?!";
                textPanel.SetActive(true);
                GetComponent<Collider>().enabled = true;
                break;
            case 1:
                text.text = "I’m starting to think the witch who sold me these rocks was toying with me.";
                break;
            case 2:
                text.text = "Oh, but that's probably what this other slip of paper is for.";
                break;
            case 3:
                text.text = "Alright, back to it...";
                break;
            case 4:
                textPanel.SetActive(false);
                GetComponent<Collider>().enabled = false;
                narrativeState++;
                advanceState = -1;
                break;
        }
    }

    public void Narrative2()
    {
        switch (advanceState)
        {
            case 0:
                text.text = "It summoned a compass, now I can get home!";
                compass.SetActive(true);
                textPanel.SetActive(true);
                GetComponent<Collider>().enabled = true;
                break;
            case 1:
                text.text = "Oh... If only I had my keys...";
                break;
            case 2:
                winPanel.SetActive(true);
                GetComponent<Collider>().enabled = false;
                break;
        }
    }
}
