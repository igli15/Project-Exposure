using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ScoreBehaviour : MonoBehaviour {

    private string m_tag;

    public void ActivateScoreBehaviour(string tag)
    {
        m_tag = tag;
        transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

        Tweener scalUp = transform.DOScale(transform.localScale.x*1.2f, 1); 
        Tweener scaleDown = transform.DOScale(0.5f, 1);

        transform.DOMoveY(transform.position.y + 100, 2);

        Sequence s = DOTween.Sequence();
        s.Append(scalUp);
        s.Append(scaleDown);
        s.AppendCallback(Callback);
    }

    public void Callback()
    {
        ObjectPooler.instance.DestroyFromPool(m_tag, gameObject);
    }
}
