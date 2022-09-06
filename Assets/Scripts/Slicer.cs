using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Slicer : MonoBehaviour
{
    public LayerMask mask;

    void Update()
    {
    }

    public SlicedHull Cut(GameObject obj, Material mat = null)
    {
        return obj.Slice(transform.position, transform.up, mat);
    }

    public GameObject SliceCube()
    {
        Collider[] objectsToCut = Physics.OverlapBox(transform.position, new Vector3(1f, 0.1f, 1f), transform.rotation, mask);


        foreach (Collider item in objectsToCut)
        {
            Material mat = item.GetComponent<MeshRenderer>().material;



            SlicedHull cutObject = Cut(item.gameObject);
            if (cutObject == null)
            {
                return null;
            }
            GameObject cutUp = cutObject.CreateUpperHull(item.gameObject, mat);
            GameObject cutDown = cutObject.CreateLowerHull(item.gameObject, mat);

            Destroy(item.gameObject);

            cutUp.AddComponent<MeshCollider>().convex = true;
            cutUp.AddComponent<Rigidbody>();
            Destroy(cutUp, 3f);
            cutDown.layer = LayerMask.NameToLayer("Default");
            Destroy(cutDown.GetComponent<MeshCollider>());

            return cutDown;
        }

        return null;
    }
}
