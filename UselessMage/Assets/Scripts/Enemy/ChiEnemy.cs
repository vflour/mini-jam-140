using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChiEnemy :Enemy 
{
    public GameObject[] aoeRadiusObjects;
    private GameObject aoeRadiusFX;
    
    public float elementSwitchCooldownTime = 5f;
    private float _elementSwitchCooldown;

    protected override void Start()
    {
        base.Start();
        effectivenessMultiplier = new Dictionary<ElementType, float> {
            { ElementType.Fire, 0 },
            { ElementType.Grass, 0 },
            { ElementType.Ice, 0 },
        };

        _elementSwitchCooldown = elementSwitchCooldownTime;
    }

    private ElementType GetEffectiveType()
    {
        return aoeLogic.effectiveness.FirstOrDefault(e => e.Value == elementType).Key;
    }

    private void HandleElementSwitching()
    {
        _elementSwitchCooldown -= Time.deltaTime;
        var distance = (target.transform.position - transform.position).magnitude;
        if (_elementSwitchCooldown <= 0)
        {   
            _elementSwitchCooldown = elementSwitchCooldownTime;
            if (elementType != ElementType.Neutral)
                effectivenessMultiplier[GetEffectiveType()] = 0;
            
            elementType = (ElementType) (((int)elementType + 1) % 3);
            effectivenessMultiplier[GetEffectiveType()] = 1f;
            
            if (aoeRadiusFX)
                Destroy(aoeRadiusFX);

            aoeRadiusFX = Instantiate(aoeRadiusObjects[(int)elementType], transform);
            aoeRadiusFX.transform.localScale = new Vector3(2, 2, 2);
        }
    }
    
    protected override void FollowTarget()
    {
        base.FollowTarget();
        var direction = Vector3.Normalize(rb.velocity);
        enemyAnimator.SetFloat("Horizontal", direction.x);
        enemyAnimator.SetFloat("Vertical", direction.y);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (EnemyAnnoyanceState != AnnoyanceState.Enraged)
            HandleElementSwitching();
    }

}
