using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCatPunch : MonoBehaviour
{
    private Coroutine catPunchCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster"))
        {
            StartCoroutine(HitMonster(other));
            //other.GetComponent<Monster>().Die();
        }
    }

    public void StartCatPunchCoroutine()
    {
        // 이미 실행 중인 코루틴을 중복으로 실행하지 않도록 예방
        if (catPunchCoroutine == null)
        {
            catPunchCoroutine = StartCoroutine(CatPunch());
        }
    }

    public void StopCatPunchCoroutine()
    {
        if (catPunchCoroutine != null)
        {
            StopCoroutine(catPunchCoroutine);
            catPunchCoroutine = null;
        }
    }

    private IEnumerator CatPunch()
    {
        float moveTime = 0.0f;
        gameObject.transform.parent = null;
        while (moveTime < 3.0f)
        {
            transform.Translate(Vector3.forward * 0.1f);
            moveTime += Time.deltaTime;
            yield return null;
        }

        //yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);

        // 코루틴이 끝났을 때 catPunchCoroutine을 null로 설정
        catPunchCoroutine = null;
    }

    private IEnumerator HitMonster(Collider monster)
    {
        float moveTime = 0.0f;

        while (moveTime < 2.0f)
        {
            monster.enabled = false;
            monster.transform.Translate(gameObject.transform.forward * 0.1f);
            moveTime += Time.deltaTime;
            yield return null;
        }

        monster.GetComponent<Monster>().Die();
        StopCoroutine(HitMonster(monster));
    }
}
