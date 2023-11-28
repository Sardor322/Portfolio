using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{ 

    [SerializeField] private Transform firstObject;
    [SerializeField] private List<Transform> objects;
    [SerializeField] public Transform spawnSlot123;


    [SerializeField] private AudioSource clip;


    [Header("End Game Doors")]

    public SpriteRenderer endLeftOpen;
    public SpriteRenderer endRightOpen;
    public SpriteRenderer endLeftClose;
    public SpriteRenderer endRightClose;


    public int counterGame = 0;

    private void Start()
    {
        firstObject.transform.DOMove(transform.position, 0.88f).SetEase(Ease.OutBack);
        clip.Play();
    }

    public void MoveTopObjectToDown()
    {
        
        if (counterGame <= 10) { 
        int random = Random.Range(0, objects.Count);
        clip.Play();
        objects[random].DOMove(spawnSlot123.position, 0.88f).SetEase(Ease.OutBack).OnComplete(() => {
            objects.RemoveAt(random);
        });
        }
        if (counterGame == 11)
        {
            endLeftOpen.DOFade(255f, 0.015f);
            endRightOpen.DOFade(255f, 0.015f);
            endLeftClose.DOFade(0f, 0.15f);
            endRightClose.DOFade(0f, 0.15f);

        }
    }
    
}
