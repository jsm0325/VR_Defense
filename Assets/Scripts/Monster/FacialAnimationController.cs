using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacialAnimationController : MonoBehaviour
{
    public Animator eyeAnimator;
    public Animator eyebrowAnimator;
    public Animator mouthAnimator;
    public string eyeAnimationStateName = "Facial_Anim_eye";
    public string eyebrowAnimationStateName = "Facial_Anim_eyebrow";
    public string mouthAnimationStateName = "Facial_Anim_mouth";


    public void SetFacial(string monsterName, int facialNumber)
    {
        if (facialNumber >= 0 && facialNumber < 7)
        {
            eyeAnimator.speed = 0.0166666666666667f;
            eyebrowAnimator.speed = 0.0166666666666667f;
            mouthAnimator.speed = 0.0166666666666667f;
            if (monsterName == "NormalMonster")
            {
                eyeAnimator.Play(eyeAnimationStateName, 0, GameManager.gameManager.normalFacialData[facialNumber].eyeAnimationTime / eyeAnimator.GetCurrentAnimatorStateInfo(0).length);
                eyebrowAnimator.Play(eyebrowAnimationStateName, 0, GameManager.gameManager.normalFacialData[facialNumber].eyebrowAnimationTime / eyebrowAnimator.GetCurrentAnimatorStateInfo(0).length);
                mouthAnimator.Play(mouthAnimationStateName, 0, GameManager.gameManager.normalFacialData[facialNumber].mouthAnimationTime / mouthAnimator.GetCurrentAnimatorStateInfo(0).length);
            }
            else if (monsterName == "TiredMonster")
            {
                eyeAnimator.Play(eyeAnimationStateName, 0, GameManager.gameManager.tiredFacialData[facialNumber].eyeAnimationTime / eyeAnimator.GetCurrentAnimatorStateInfo(0).length);
                eyebrowAnimator.Play(eyebrowAnimationStateName, 0, GameManager.gameManager.tiredFacialData[facialNumber].eyebrowAnimationTime / eyebrowAnimator.GetCurrentAnimatorStateInfo(0).length);
                mouthAnimator.Play(mouthAnimationStateName, 0, GameManager.gameManager.tiredFacialData[facialNumber].mouthAnimationTime / mouthAnimator.GetCurrentAnimatorStateInfo(0).length);
            }
            else if (monsterName == "SpeedMonster")
            {
                eyeAnimator.Play(eyeAnimationStateName, 0, GameManager.gameManager.speedFacialData[facialNumber].eyeAnimationTime / eyeAnimator.GetCurrentAnimatorStateInfo(0).length);
                eyebrowAnimator.Play(eyebrowAnimationStateName, 0, GameManager.gameManager.speedFacialData[facialNumber].eyebrowAnimationTime / eyebrowAnimator.GetCurrentAnimatorStateInfo(0).length);
                mouthAnimator.Play(mouthAnimationStateName, 0, GameManager.gameManager.speedFacialData[facialNumber].mouthAnimationTime / mouthAnimator.GetCurrentAnimatorStateInfo(0).length);
            }
            else if (monsterName == "TankerMonster")
            {
                eyeAnimator.Play(eyeAnimationStateName, 0, GameManager.gameManager.tankerFacialData[facialNumber].eyeAnimationTime / eyeAnimator.GetCurrentAnimatorStateInfo(0).length);
                eyebrowAnimator.Play(eyebrowAnimationStateName, 0, GameManager.gameManager.tankerFacialData[facialNumber].eyebrowAnimationTime / eyebrowAnimator.GetCurrentAnimatorStateInfo(0).length);
                mouthAnimator.Play(mouthAnimationStateName, 0, GameManager.gameManager.tankerFacialData[facialNumber].mouthAnimationTime / mouthAnimator.GetCurrentAnimatorStateInfo(0).length);
            }



            eyeAnimator.speed = 0f;
            eyebrowAnimator.speed = 0f;
            mouthAnimator.speed = 0f;
        }
    }
}
