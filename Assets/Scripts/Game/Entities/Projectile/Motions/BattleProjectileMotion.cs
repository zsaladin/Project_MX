using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class BattleProjectileMotion : ITickable
{
    protected BattleProjectile _projectitle;
    protected float _speed;
    protected float _maxHeight;

    public void Init(BattleProjectile projectile)
    {
        _projectitle = projectile;
    }

    public abstract void OnTick();

    public virtual void Update()
    {
        _projectitle.transform.position = Vector3.MoveTowards(_projectitle.transform.position, _projectitle.Position, _speed * Time.deltaTime);
    }

    public abstract bool IsEnded();

    public static BattleProjectileMotion Create(ProjectileProfile profile)
    {
        BattleProjectileMotion motion;
        switch (profile.ProjectileMotionType)
        {
            case ProjectileMotionType.Straight:
                motion = new BattleProjectileMotion_Straight(); break;
            case ProjectileMotionType.Arc:
                motion = new BattleProjectileMotion_Arc(); break;

            default: return null;
        }

        motion._speed = profile.Speed;
        motion._maxHeight = profile.MaxHeight;
        return motion;
    }
}

public enum ProjectileMotionType
{
    None,
    Straight,
    Arc
}
