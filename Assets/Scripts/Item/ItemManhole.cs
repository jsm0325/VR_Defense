using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManhole : MonoBehaviour
{
    [SerializeField]
    private float trapDuration = 5.0f;
    [SerializeField]
    private float objectDuration = 10.0f;
    private HoverItem2 hoverItem;

    private void Awake()
    {
        hoverItem = GetComponent<HoverItem2>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (hoverItem.itemRotation == false)
        {
            StartCoroutine(DestroyManhole());
            if (other.CompareTag("Monster"))
            {
                if (transform.root.name.Equals("OVRPlayerController") == false)
                {
                    Trap(other);
                }
            }
        }
    }

    private void Trap(Collider monster)
    {
        monster.GetComponent<Monster>().trapDuration = trapDuration;
        //monster.GetComponent<Monster>().isTrapped = true;
        monster.GetComponent<Monster>().SetTrapped(trapDuration);
    }

    private IEnumerator DestroyManhole()
    {
        yield return new WaitForSeconds(objectDuration);
        Destroy(gameObject);
    }
}
