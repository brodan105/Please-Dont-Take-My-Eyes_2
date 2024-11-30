using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSpikeController : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ExtendLeft()
    {
        StartCoroutine(ELeft());
    }

    public void ExtendRight()
    {
        StartCoroutine(ERight());
    }

    public void RetractLeft()
    {
        StartCoroutine(RLeft());
    }

    public void RetractRight()
    {
        StartCoroutine(RRight());
    }

    IEnumerator ELeft()
    {
        yield return new WaitForSeconds(3f);
        anim.SetTrigger("LeftRise");
    }

    IEnumerator ERight()
    {
        yield return new WaitForSeconds(3f);
        anim.SetTrigger("RightRise");
    }

    IEnumerator RLeft()
    {
        yield return new WaitForSeconds(15f);
        anim.SetTrigger("LeftFall");
    }

    IEnumerator RRight()
    {
        yield return new WaitForSeconds(15f);
        anim.SetTrigger("RightFall");
    }
}
