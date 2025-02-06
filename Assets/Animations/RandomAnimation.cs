using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimation : MonoBehaviour
{
    public Animator myAnimations = new Animator();
    void Start()
    {
        myAnimations = GetComponent<Animator>();
        var elementrandom = Random.Range(0, 2);
        print(elementrandom);
        myAnimations.SetInteger("typeanim",elementrandom);
    }
}
//