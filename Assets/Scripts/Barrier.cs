using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barrier : MonoBehaviour
{
    public float hp = 1;
    public Image hpBar;
    public GameObject obstacle;

    public GameObject destroyParticles;

    public List<EscaperBase> crushers = new List<EscaperBase>();

    public void DecreaseHP()
    {
        hp -= 0.1f;
        hpBar.fillAmount = hp;
        if (hp <= 0)
        {
            for (int i = 0; i < crushers.Count; i++)
            {
                if (crushers[i] != null)
                    crushers[i].OnBarrierBroke();
            }
            crushers.Clear();
            destroyParticles.SetActive(true);

            Destroy(obstacle);
            Destroy(gameObject);
        }
    }
}