using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public Color targetColor;
    private Color lastColor;
    [Header("How many cubes will it take to change the target color")]
    public int amount;

    public int cubeCounter;
    //public GameObject[] planes;
    public GameObject[] initialCubes;

    public GameObject lastCube;
    

    public void Initialize()
    {
        initialCubes[cubeCounter++].GetComponent<MeshRenderer>().material.color = Color.white;
        //targetColor = Random.ColorHSV();
        targetColor = new Color(Random.Range(0.3f, 0.9f), Random.Range(0.3f, 0.9f), Random.Range(0.3f, 0.9f));
        lastColor = Color.white;
        for (int i = 1; i < initialCubes.Length; i++)
        {
            initialCubes[i].GetComponent<MeshRenderer>().material.color = Color.Lerp(lastColor, targetColor, (float)cubeCounter++ % amount / amount);
            if (cubeCounter % amount == 0)
            {
                lastColor = targetColor;
                targetColor = new Color(Random.Range(0.3f, 0.9f), Random.Range(0.3f, 0.9f), Random.Range(0.3f, 0.9f));
                //targetColor = Random.ColorHSV();
            }
        }

        lastCube = initialCubes[initialCubes.Length - 1];

        /*for (int i = planes.Length - 1; i >= 0; i--)
        {
            planes[i].GetComponent<MeshRenderer>().material.color =
                initialCubes[(initialCubes.Length - 1) - (planes.Length - i)].GetComponent<MeshRenderer>().material.color;
        }*/
    }

    public Color GenerateColor()
    {
        Color newColor = Color.Lerp(lastColor, targetColor, (float)cubeCounter++ % amount / amount);
        if (cubeCounter % amount == 0)
        {
            targetColor = new Color(Random.Range(0.3f, 0.9f), Random.Range(0.3f, 0.9f), Random.Range(0.3f, 0.9f));
        }

        /*for (int i = 0; i < planes.Length - 1; i++)
        {
            planes[i].GetComponent<MeshRenderer>().material.color = planes[i + 1].GetComponent<MeshRenderer>().material.color;
        }
        planes[(planes.Length - 1)].GetComponent<MeshRenderer>().material.color = newColor;*/

        lastColor = newColor;

        return newColor;
    }
}
