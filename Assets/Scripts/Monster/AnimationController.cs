using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator headAnimator;
    public Animator bodyAnimator;
    private Animator hairAnimator;
    private Animator topAnimator;
    private Animator bottomAnimator;
    private Animator shoeAnimator;


    public void setKnockBack()
    {
        headAnimator.SetTrigger("knockBack");
        bodyAnimator.SetTrigger("knockBack");
        hairAnimator.SetTrigger("knockBack");
        topAnimator.SetTrigger("knockBack");
        bottomAnimator.SetTrigger("knockBack");
        shoeAnimator.SetTrigger("knockBack");

    }

    public void SetisTrapped(bool input)
    {
        headAnimator.SetBool("isTrapped", input);
        bodyAnimator.SetBool("isTrapped", input);
        hairAnimator.SetBool("isTrapped", input);
        topAnimator.SetBool("isTrapped", input);
        bottomAnimator.SetBool("isTrapped", input);
        shoeAnimator.SetBool("isTrapped", input);

    }

    public void SetAivityKitty(bool input)
    {
        headAnimator.SetBool("isTrapped", input);
        bodyAnimator.SetBool("isTrapped", input);
        hairAnimator.SetBool("isTrapped", input);
        topAnimator.SetBool("isTrapped", input);
        bottomAnimator.SetBool("isTrapped", input);
        shoeAnimator.SetBool("isTrapped", input);
    }

    public void SetAnimator(int number, Animator inputAnimator)
    {
        if (number == 0)
        {
            hairAnimator = inputAnimator;
        }
        else if(number == 1)
        {
            topAnimator = inputAnimator;
        }
        else if (number == 2)
        {
            bottomAnimator = inputAnimator;
        }
        else if (number == 3)
        {
            shoeAnimator = inputAnimator;
        }
    }
}
