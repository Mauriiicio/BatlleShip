using System.Collections;
using UnityEngine;

public class FireAnimation : MonoBehaviour
{
    void Start()
    {
        GetComponent<Animator>().Play("FireAnimation");
    }
    public void DestrObject()
    {
        Destroy(gameObject);
    }
}