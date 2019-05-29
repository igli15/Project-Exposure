using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ScoreBehaviour : MonoBehaviour {
    private string m_tag;
    public void ActivateScoreBehaviour(string tag)
    {
        m_tag = tag;
        Tweener scalUp = transform.DOScale(1.5f, 1); 
        Tweener scaleDown = transform.DOScale(0.5f, 1);
        transform.DOMoveY(transform.position.y + 100, 2);
        Sequence s = DOTween.Sequence();
        s.Append(scalUp);
        s.Append(scaleDown);
        s.AppendCallback(Callback);
        
        //transform.DOMoveY(transform.position.y + 10, 4);
    }

    public void Callback()
    {
        ObjectPooler.instance.DestroyFromPool(m_tag, gameObject);
    }
}
