using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMonster : MonoBehaviour
{
    public AnimationController animController;
    public Animator monsterAnim;
    // 외형 카테고리들을 저장할 배열의 배열
    public GameObject[][] appearanceOptions;
    // 외형 카테고리들
    public GameObject[] hairOptions;
    public GameObject[] topOptions;
    public GameObject[] bottomOptions;
    public GameObject[] shoeOptions;
    public Vector3 clothsScale = new Vector3(11.0f, 11.0f, 9.0f);
    // Start is called before the first frame update
    void Start()
    {
        InitializeAppearanceOptions(); // 외형 카테고리 배열 초기화
        SetRandomAppearance(); // 랜덤하게 바디, 의상, 헤어 등을 선택하여 적용
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeAppearanceOptions()
    {
        // 외형 카테고리 배열 생성과 초기화
        appearanceOptions = new GameObject[4][];
        appearanceOptions[0] = hairOptions;
        appearanceOptions[1] = topOptions;
        appearanceOptions[2] = bottomOptions;
        appearanceOptions[3] = shoeOptions;
    }

    private void SetRandomAppearance() // 랜덤 의상 생성 코드
    {
        for (int i = 0; i < appearanceOptions.Length; i++)
        {
            // 해당 카테고리의 배열 길이가 0 이상인 경우에만 랜덤한 인덱스 선택
            if (appearanceOptions[i].Length > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, appearanceOptions[i].Length);
                GameObject selectedAppearancePrefab = appearanceOptions[i][randomIndex];
                GameObject selectedAppearance = Instantiate(selectedAppearancePrefab, transform.position, transform.rotation, transform);
                selectedAppearance.transform.localScale = clothsScale;
                Animator appearanceAnim = selectedAppearance.GetComponent<Animator>();
                if (appearanceAnim == null)
                {
                    appearanceAnim = selectedAppearance.AddComponent<Animator>();
                    animController.SetAnimator(i, appearanceAnim);
                }

                appearanceAnim.runtimeAnimatorController = GetComponent<Animator>().runtimeAnimatorController;


            }

        }
    }
}
