using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cube : MonoBehaviour
{
    void Start()
    {
        GetComponent<MeshRenderer>().material.color = GameObject.Find("ColorManager").GetComponent<ColorManager>().GenerateColor();
    }


    void Update()
    {

    }

    public void Move(GameManager.Direction direction, float target)
    {
        if (direction.Equals(GameManager.Direction.x))
        {
            transform.DOMoveX(target, 1.5f).OnComplete(() => Move(direction, -target)).SetEase(Ease.Linear);
        }
        else if (direction.Equals(GameManager.Direction.z))
        {
            transform.DOMoveZ(target, 1.5f).OnComplete(() => Move(direction, -target)).SetEase(Ease.Linear);
        }
    }

    public void Pause()
    {
        transform.DOKill();
    }

    public void Perfect()
    {
        GameObject perfectEffect = transform.GetChild(0).gameObject;
        MeshRenderer renderer = perfectEffect.GetComponent<MeshRenderer>();
        transform.DetachChildren();

        perfectEffect.SetActive(true);

        perfectEffect.transform.localScale =
            new Vector3(transform.localScale.x + 0.3f, 0.001f, transform.localScale.z + 0.3f);

        Color temp = renderer.material.color;
        temp.a = 0.2f;
        renderer.material.DOColor(temp, 0.6f).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(perfectEffect);
        });
    }
}
