using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{ 

    [SerializeField] private Transform firstObject;
    [SerializeField] private List<Transform> objects;
    [SerializeField] public Transform spawnSlot123;

    private void Start()
    {
        firstObject.transform.DOMove(transform.position, 0.88f).SetEase(Ease.OutBack);
    }

    public void MoveTopObjectToDown()
    {
        int random = Random.Range(0, objects.Count);   
        objects[random].DOMove(spawnSlot123.position, 0.88f).SetEase(Ease.OutBack).OnComplete(() => {
            objects.RemoveAt(random);
            });
    }
}
