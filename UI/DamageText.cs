using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{

    float moveSpeed;
    float alphaSpeed;
    float destroyTime;

    TextMeshPro text;
    Color alpha;
    public int damage;
    private void Start()
    {
        moveSpeed = 10.0f;
        alphaSpeed = 2.0f;
        destroyTime = 1.5f;

        text = GetComponent<TextMeshPro>();
        alpha = text.color;
        text.text = damage.ToString();
        Invoke("DestroEvent", destroyTime);
        
    }
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;

    }
    public void DestroEvent()
    {
        Destroy(gameObject);
    }
}
