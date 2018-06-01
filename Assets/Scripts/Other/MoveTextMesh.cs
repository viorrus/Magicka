using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveTextMesh : MonoBehaviour {

    public float deltaY;
    public float duration;
    private TextMesh textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMesh>();
        Sequence sequence = DOTween.Sequence();
        sequence.SetDelay(duration / 2)
                .Join(transform.DOMoveY(transform.position.y + deltaY, duration))
            .Append(DOTween.To(() => textMesh.color, x => textMesh.color = x, Color.clear, duration / 2));
           
    }
}
