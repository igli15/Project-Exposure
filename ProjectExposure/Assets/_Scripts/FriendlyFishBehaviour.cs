using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FriendlyFishBehaviour : Hittable {


    CurveWallker m_wallker;

    private void Start()
    {
        m_wallker = GetComponent<CurveWallker>();
    }
    public override void Hit(AbstractGun gun, float damage)
    {
        StunFish();
    }

    public void StunFish()
    {
        m_wallker.StopMovement();
        ScoreStats.instance.comboMeter.BreakCombo();
        transform.DOScale(1, 2).OnComplete(()=> { m_wallker.StartMovement(); });
    }

    public override void SetColor(Color newColor)
    {
        base.SetColor(newColor);
    }

}
