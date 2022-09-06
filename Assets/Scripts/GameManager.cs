using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject cubePrefab;
    private GameObject newCube;

    [SerializeField]
    Slicer[] slicers;

    [SerializeField]
    ColorManager colorManager;
    [SerializeField]
    CameraFollow cameraFollow;

    GameObject newPrefab;

    public int score;
    public TextMeshProUGUI scoreText;

    [SerializeField]
    float perfectOffset;

    public enum Direction
    {
        x, z
    };

    Direction direction;

    void Start()
    {
        colorManager.Initialize();
        cubePrefab.transform.localScale = new Vector3(3f, 0.5f, 3f);
        newPrefab = colorManager.lastCube;
        NewCube();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (newCube.transform.position.x > colorManager.lastCube.transform.position.x - perfectOffset
                && newCube.transform.position.x < colorManager.lastCube.transform.position.x + perfectOffset
                && newCube.transform.position.z < colorManager.lastCube.transform.position.z + perfectOffset
                && newCube.transform.position.z > colorManager.lastCube.transform.position.z - perfectOffset)
            {
                Score();
                Debug.Log("Perfectly Fit");
                newCube.transform.position =
                    new Vector3(colorManager.lastCube.GetComponent<MeshRenderer>().bounds.center.x,
                    newCube.transform.position.y, colorManager.lastCube.GetComponent<MeshRenderer>().bounds.center.z);
                newCube.GetComponent<Cube>().Pause();
                newCube.GetComponent<Cube>().Perfect();
                colorManager.lastCube = newCube;
                NewCube();
                return;
            }
            for (int i = 0; i < slicers.Length; i++)
            {
                newPrefab = slicers[i].SliceCube();
                if (newPrefab != null)
                {
                    Score();
                    colorManager.lastCube = newPrefab;
                    MeshRenderer lastMesh = newPrefab.GetComponent<MeshRenderer>();
                    cubePrefab.transform.localScale = lastMesh.bounds.extents * 2;
                    slicers[0].gameObject.transform.position = new Vector3(lastMesh.bounds.center.x + lastMesh.bounds.extents.x, slicers[0].gameObject.transform.position.y, lastMesh.bounds.center.z);
                    slicers[1].gameObject.transform.position = new Vector3(lastMesh.bounds.center.x - lastMesh.bounds.extents.x, slicers[1].gameObject.transform.position.y, lastMesh.bounds.center.z);
                    slicers[2].gameObject.transform.position = new Vector3(lastMesh.bounds.center.x, slicers[2].gameObject.transform.position.y, lastMesh.bounds.center.z - lastMesh.bounds.extents.z);
                    slicers[3].gameObject.transform.position = new Vector3(lastMesh.bounds.center.x, slicers[3].gameObject.transform.position.y, lastMesh.bounds.center.z + lastMesh.bounds.extents.z);
                    newCube.GetComponent<Cube>().Pause();
                    NewCube();
                    return;
                }
            }
            if (newPrefab == null)
            {
                newCube.GetComponent<Cube>().Pause();
                Destroy(newCube.GetComponent<Cube>());
                newCube.AddComponent<Rigidbody>();
                Invoke(nameof(EndGame), 2f);
            }

        }
    }

    private void Score()
    {
        score++;
        scoreText.text = score.ToString();
    }

    private void EndGame()
    {
        SceneManager.LoadScene(0);
    }

    private void NewCube()
    {

        direction = colorManager.cubeCounter % 2 == 1 ? Direction.x : Direction.z;
        float yValue = colorManager.lastCube.transform.position.y + 0.5f;
        if (direction.Equals(Direction.x))
        {
            newCube = Instantiate(cubePrefab, new Vector3(-4f, yValue, newPrefab.GetComponent<MeshRenderer>().bounds.center.z), Quaternion.identity);
            newCube.layer = LayerMask.NameToLayer("CuttableX");
        }

        if (direction.Equals(Direction.z))
        {
            newCube = Instantiate(cubePrefab, new Vector3(newPrefab.GetComponent<MeshRenderer>().bounds.center.x, yValue, -4f), Quaternion.identity);
            newCube.layer = LayerMask.NameToLayer("CuttableZ");
        }


        cameraFollow.SetTarget();

        newCube.GetComponent<Cube>().Move(direction, 4);
    }

}
