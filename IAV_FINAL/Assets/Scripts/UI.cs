using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public RectTransform hpBar;
    private float hpBarWitdh;
    private float maxHp=100;
    private float hp;
    // Start is called before the first frame update
    void Start()
    {
        hpBarWitdh = hpBar.sizeDelta.x;
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && hp < 100)
        {
            hp += 10;
            UpdateHP();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            hp -= 10;
            UpdateHP();
        }
    }

    public void UpdateHP()
    {
        Vector2 size = hpBar.sizeDelta;
        size.x = hp / maxHp * hpBarWitdh;
        hpBar.sizeDelta = size;

    }
}
