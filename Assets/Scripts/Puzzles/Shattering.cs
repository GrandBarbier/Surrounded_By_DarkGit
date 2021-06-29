using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shattering : MonoBehaviour
{
    //public GameObject destroyedVersion;

    private GameObject[] childs;
    private bool shatered;
    public Vector3 breakingForce;

    void Start()
    {
        childs = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            childs[i] = transform.GetChild(i).gameObject;
        }
    }
    void Shatter()
    {
        /*if (destroyedVersion)
        {
            Debug.Log("Shatter");
            GameObject g = Instantiate(destroyedVersion, transform.position, transform.rotation);
            g.transform.localScale = transform.localScale;
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("No Destroyed version");
        }*/

        if (!shatered)
        {
            foreach (var child in childs)
            {
                Rigidbody r = child.AddComponent<Rigidbody>();
                r.AddForce(breakingForce);

                if (Gears.gears?.somkeTrail)
                {
                    GameObject g = Instantiate(Gears.gears.somkeTrail, child.transform.position, child.transform.rotation);
                    g.transform.SetParent(child.transform);

                    if (g.TryGetComponent(out ParticleSystem particleSystem))
                    {
                        var v = particleSystem.shape;
                        v.enabled = true;
                        v.shapeType = ParticleSystemShapeType.Mesh;

                        if (child.TryGetComponent(out MeshFilter meshFilter))
                        {
                            v.mesh = meshFilter.mesh;
                        }
                    }
                }
            }
            
            shatered = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log($"Collision : {collision.gameObject.name}");
        // if (collision.gameObject.TryGetComponent(out pushableBlock pushableBlock) || MenuManager.GetAllComponentInChilds<pushableBlock>(collision.gameObject).Capacity > 0)
        // {
        //     Shatter();
        // }
        
        if(collision.gameObject.CompareTag("PushableBlock"))
        {
            Shatter();
        }
    }
}
