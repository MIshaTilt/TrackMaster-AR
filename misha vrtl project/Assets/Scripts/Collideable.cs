using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collideable : MonoBehaviour
{
    public Rigidbody rigidbody;
    public List<Collider> colliders;

    // Start is called before the first frame update
    void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.isKinematic = true;
        rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        foreach (var collider in colliders)
        {
            collider.isTrigger = true;
        }
        transform.gameObject.tag = "propsActive";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("car"))
        {
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
            foreach (var collider in colliders)
            {
                collider.isTrigger = false;
            }
            transform.parent = null;
            var light = transform.GetComponentInChildren<Light>();
            if(light != null)
            {
                light.enabled = false;
            }
            StartCoroutine(Delete());
        }
    }

    private IEnumerator Delete()
    {
        yield return new WaitForSeconds(15);
        gameObject.SetActive(false);
    }
}
