using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using TMPro;

public class ResourceCollectedIcon : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public TMP_Text resourceText;

    private void Start()
    {
        animator = GetComponent<Animator>();
        Invoke("destroyAnimation", 1);
    }

    private void Update()
    {
        resourceText.color = this.GetComponent<SpriteRenderer>().color;
    }

    private void destroyAnimation()
    {
        Destroy(this.gameObject);
    }

    public void updateResourceText(int resources)
    {
        resourceText.text = ("+" + resources).ToString();
    }
}
