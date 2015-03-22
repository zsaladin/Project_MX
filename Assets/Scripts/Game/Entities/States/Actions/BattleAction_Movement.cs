using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleAction_Movement : BattleAction
{
    private List<Vector3> _pathPositions;
    public override ActionType Type
    {
        get
        {
            return ActionType.Movement;
        }
    }

    public override void OnBegin()
    {
        base.OnBegin();

        Animation animation = Actor.GetComponent<Animation>();
        animation.Stop();
        animation.Play("Walk");
    }

    public override void OnTick()
    {
        //Debug.Log(string.Format("{0} : {1}", Actor.name, GetType().Name));
        _pathPositions = Manager.Coordinate.GetPath(Actor);
    }

    public override void OnEnd()
    {
        
    }

    public override void Update()
    {
        if (_pathPositions == null) return;
        if (_pathPositions.Count == 0) return;

        Vector3 destination = _pathPositions[_pathPositions.Count -1];
        Actor.transform.position = Vector3.MoveTowards(Actor.transform.position, destination, 1 * Time.deltaTime);
        Actor.transform.LookAt(destination);
    }
    
}


public enum MovementType
{
    Invalid     = 0x00,
    Normal      = 0x01,
    Flying      = 0x02,
}