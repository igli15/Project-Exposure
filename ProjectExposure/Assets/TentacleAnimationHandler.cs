using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TentacleAnimationHandler : MonoBehaviour {

    public void ShakeCamera()
    {
        Camera.main.transform.DOShakePosition(1, 0.02f);
    }

}
