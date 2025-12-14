using System;
using System.Collections;
using UnityEngine;

public class ActionConductor : IActionConductor {
    public ActionConductor() {
        
    }

    public IEnumerator DelayedAction(Action delayedAction, float timeToDelaySeconds) {
        yield return new WaitForSeconds(timeToDelaySeconds);
        delayedAction();
    }

    public IEnumerator DelayedSequence(Action preDelayAction, Action delayedAction, float timeToDelaySeconds) {
        preDelayAction();
        yield return new WaitForSeconds(timeToDelaySeconds);
        delayedAction();
    }
}