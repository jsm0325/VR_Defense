using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPaperTowel : MonoBehaviour
{
    //[SerializeField]
    //private int damage = 1;
    [SerializeField]
    private float cooldown = 7;
    [SerializeField]
    private float force = 15f;

    [SerializeField]
    private GameObject paperToewlPrefab;
    public bool isCooldown = false;
    private GameObject paperTowel;

    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = gameObject.transform.root;
    }

    public IEnumerator PaperTowel()
    {
        isCooldown = true;
        Vector3 playerPosition = playerTransform.transform.position;
        Vector3 playerForward = playerTransform.transform.forward;
        paperTowel = Instantiate(paperToewlPrefab, playerPosition + playerForward + Vector3.up, playerTransform.rotation);
        Rigidbody rigidBody = paperTowel.GetComponent<Rigidbody>();

        if (rigidBody != null)
        {
            rigidBody.AddForce(playerForward * force, ForceMode.Impulse);
        }

        StartCoroutine(DestroyPrefab());

        yield return new WaitForSeconds(cooldown);

        isCooldown = false;
    }

    private IEnumerator DestroyPrefab()
    {
        yield return new WaitForSeconds(3f);

        Destroy(paperTowel);
    }
}
