using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{

    float moveSpeed;
    float alphaSpeed;
    float destroyTime;

    Text text;
    Color alpha;
    public int damage;
    private void Start()
    {
        moveSpeed = 10.0f;
        alphaSpeed = 2.0f;
        destroyTime = 1.5f;

        text = GetComponent<Text>();
        //text.text = damage.ToString();
        Invoke("DestroEvent", destroyTime);
        
    }
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);

    }
    public void DestroEvent()
    {
        Destroy(gameObject);
    }
}
