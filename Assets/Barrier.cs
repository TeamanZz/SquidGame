using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barrier : MonoBehaviour
{
    public float hp = 1;
    public Image hpBar;

    public List<Escaper> crushers = new List<Escaper>();

    public void DecreaseHP()
    {
        hp -= 0.1f;
        hpBar.fillAmount = hp;
        if (hp <= 0)
        {
            for (int i = 0; i < crushers.Count; i++)
            {
                crushers[i].OnBarrierBroke();
            }
            crushers.Clear();
            Destroy(gameObject);
        }
    }
}