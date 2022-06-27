using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    // Start is called before the first frame update
    private RectTransform hpBar;
    private float hpBarWidth;
    void Start()
    {
        hpBar = transform.Find("HpBar_front").GetComponent<RectTransform>();
        hpBarWidth = hpBar.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    public void SetHp(float hp, float maxHp)
    {
        Vector2 size = hpBar.sizeDelta;
        size.x = hp / maxHp * hpBarWidth;
        hpBar.sizeDelta = size;
    }
}
