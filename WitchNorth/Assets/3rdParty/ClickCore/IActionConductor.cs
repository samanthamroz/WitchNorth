using System;
using System.Collections;

public interface IActionConductor {
    public IEnumerator DelayedAction(Action delayedAction, float timeToDelaySeconds);
    public IEnumerator DelayedSequence(Action preDelayAction, Action delayedAction, float timeToDelaySeconds);
}