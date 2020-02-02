using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorsAssemblyBehavior : MonoBehaviour
{
    public GameObject   motor;
    public float        thrust;
    public int          numPlayers;
    public float        startupGraceTime = 5f;
    public float        shiftingPeriod = 4f;
    public float        periodBetweenShifts = 8f;

    private float       remainingStateTime;
    public enum SelectState {
        GRACE = 0,
        SHIFTING = 1,
        SELECTED = 2,
        INVALID = 3,
    }
    private SelectState currentState = SelectState.GRACE;
    private SelectState nextState    = SelectState.SHIFTING;

    private delegate float StateTimeLookup();
    private delegate void StateHandler();
    private StateTimeLookup[]   stateTimeLookup;    
    private StateHandler[]      stateHandlerLookup;
    private SelectState[]       followingStateLookup = {
        /* GRACE */     SelectState.SHIFTING,
        /* SHIFTING */  SelectState.SELECTED,
        /* SELECTED */  SelectState.SHIFTING,
    };

    private Transform[] motors;
    private const int numMotors = 4;

    // Start is called before the first frame update
    void Start()
    {
        // Create some lookup tables
        stateTimeLookup = new StateTimeLookup[]{
            /* GRACE */     () => startupGraceTime,
            /* SHIFTING */  () => shiftingPeriod,
            /* SELECTED */  () => periodBetweenShifts,
        };
        stateHandlerLookup = new StateHandler[]{
            /* GRACE */     () => {},
            /* SHIFTING */  EnterShiftingState,
            /* SELECTED */  EnterSelectedState,
        };

        // Create/Instantiate motors
        motors = new Transform[numMotors];
        var origin = new Vector3(0,0,0);
        for (int i=0; i != numMotors; ++i) {
            var m = Instantiate(motor, origin, Quaternion.Euler(0,0,90f*i));
            m.name = $"Motor {i}";
            m.transform.parent = transform;
            motors[i] = m.transform;
        }

        SetTimeForState();
    }

    private void SetTimeForState() {
        remainingStateTime = stateTimeLookup[(int)currentState]();
    }

    private void SetNextState() {
        nextState = followingStateLookup[(int)currentState];
    }

    void FixedUpdate()
    {
        remainingStateTime -= Time.fixedDeltaTime;
        if (remainingStateTime < 0) {
            currentState = nextState;
            SetNextState();
            SetTimeForState();
            stateHandlerLookup[(int)currentState]();
        }
    }

    void EnterShiftingState() {
        Debug.Log("Entered shifting state");
    }

    void EnterSelectedState() {
        Debug.Log("Entered selected state");
    }
}
