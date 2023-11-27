using DG.Tweening;
using System.Collections;
using UnityEngine;

public class MoteToStart : MonoBehaviour
{
    public Transform MoveToStart;

    public float TimeForWait;

    void Start()
    {
        MoveWithTween();
    }

    public void MoveWithTween()
    {
        if (Vector2.Distance(transform.position, MoveToStart.position) > 2f)
        {
            StartCoroutine(WaitForSecondsBro());

        }
    }
    public IEnumerator WaitForSecondsBro()
    {
        yield return new WaitForSeconds(TimeForWait);
        transform.DOMove(MoveToStart.position, 1f).SetEase(Ease.OutBack);
    }

    public void initialPosition()
    {
        if (Vector2.Distance(transform.position, MoveToStart.position) > 2f)
        {
            transform.DOMove(MoveToStart.position, 1f).SetEase(Ease.OutBack);
        }
    }
}
