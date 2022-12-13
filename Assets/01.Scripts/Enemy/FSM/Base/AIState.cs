using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    public List<AIAction> actions = new List<AIAction>();
    public List<AITransition> transitions = new List<AITransition>();

    private AIBrain brain;

    private void Awake()
    {
        brain = transform.parent.parent.GetComponent<AIBrain>();
    }

    public void StateUpdate()
    {
        foreach(AIAction action in actions)
        {
            action.TakeAction();
        }

        foreach(AITransition transition in transitions)
        {
            bool result = false;

            foreach(AIDecision decision in transition.decisions)
            {
                result = decision.MakeADecision();

                if(!result)
                {
                    break;
                }
            }

            if(result)
            {
                if(transition.positiveResult != null)
                {
                    brain.ChangeState(transition.positiveResult);
                }
            }
            else
            {
                if(transition.negativeResult != null)
                {
                    brain.ChangeState(transition.negativeResult);
                }
            }
        }
    }
}
