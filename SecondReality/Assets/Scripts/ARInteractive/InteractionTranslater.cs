using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTranslater : MonoBehaviour
{
    static GameObject intObject;
    public static void Translate(GameObject interactObject)
    {
        intObject = interactObject;
        string name = interactObject.name;
        var parts = name.Split('_');
        Debug.Log(parts.Length);
        
        string objectName = parts[0];

        switch (objectName)
        {
            case "Button": DoButton(parts);
                break;
        }
    }

    private static void DoButton(string[] parts)
    {
        Debug.Log("Do button");
        string component = parts[1];
        switch (component)
        {
            case ("Animation"):
                {
                    Debug.Log("case animator");
                    Animator animator = intObject.GetComponent<Animator>();
                    //string name = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

                    Debug.Log(animator.GetCurrentAnimatorStateInfo(0));
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName(parts[2]))
                    {
                        Debug.Log("Play animation - "+parts[2]);
                        animator.Play(parts[2]);
                    }
                    else
                    {
                        Debug.Log("play Idle");
                        animator.Play("Idle");
                    }
                }
                break;
        }
    }

    //Пример названия объекта "Button_Animation_Default_Play"
}
