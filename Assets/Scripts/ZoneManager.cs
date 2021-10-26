using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ZoneManager : MonoBehaviour
{
    public static ZoneManager Instance;

    [Header("Конечные ворота")]

    public Transform leftEndGate;
    public Transform rightEndGate;

    [Header("Валюта")]
    public TextMeshProUGUI moneyText;
    public int moneyCount;

    [HideInInspector] public List<Warden> wardens = new List<Warden>();
    [HideInInspector] public List<Escaper> leftZoneEscapers = new List<Escaper>();
    [HideInInspector] public List<Escaper> rightZoneEscapers = new List<Escaper>();

    private EscapersSpawner escapersSpawner;

    private void Awake()
    {
        Instance = this;
        escapersSpawner = GetComponent<EscapersSpawner>();
        moneyText.text = moneyCount + " $";
    }

    public void UpdateMoneyCount()
    {
        moneyCount++;
        moneyText.text = moneyCount + " $";
    }

    public void AddWardenToList(Warden warden)
    {
        wardens.Add(warden);
    }

    public void RemoveWardenFromList(Warden warden)
    {
        wardens.Remove(warden);
    }

    public Transform GetGate(GameZone gameZone)
    {
        if (gameZone == GameZone.Left)
            return leftEndGate;
        else
            return
            rightEndGate;
    }

    public void RemoveEscaperFromList(Escaper escaper, GameZone gameZone)
    {
        if (gameZone == GameZone.Left)
        {
            leftZoneEscapers.Remove(escaper);
        }
        else
        {
            rightZoneEscapers.Remove(escaper);
        }
        UpdateMoneyCount();
        SetNewTargetsToWardens();
    }

    public void AddEscaperToList(Escaper escaper, GameZone gameZone)
    {
        if (gameZone == GameZone.Left)
        {
            leftZoneEscapers.Add(escaper);
        }
        else
        {
            rightZoneEscapers.Add(escaper);
        }

        SetNewTargetsToWardens();
    }

    private void SetNewTargetsToWardens()
    {
        for (int i = 0; i < wardens.Count; i++)
        {
            wardens[i].SetTarget();
        }
    }

    public List<Escaper> GetEscapersList(GameZone gameZone)
    {
        if (gameZone == GameZone.Left)
            return leftZoneEscapers;
        else
            return rightZoneEscapers;
    }

    public Escaper GetNearestToGateEscaper(GameZone gameZone)
    {
        float escaperZPos = 0;
        var distance = Mathf.Infinity;
        Escaper escaper = null;
        if (gameZone == GameZone.Left)
        {
            for (int i = 0; i < leftZoneEscapers.Count; i++)
            {
                escaperZPos = leftZoneEscapers[i].transform.position.z;
                var newDist = Mathf.Abs(escaperZPos - leftEndGate.position.z);
                if (newDist < distance)
                {
                    distance = escaperZPos;
                    escaper = leftZoneEscapers[i];
                }
            }
        }
        else
        {
            for (int i = 0; i < rightZoneEscapers.Count; i++)
            {
                escaperZPos = rightZoneEscapers[i].transform.position.z;
                var newDist = Mathf.Abs(escaperZPos - rightEndGate.position.z);
                if (newDist < distance)
                {
                    distance = escaperZPos;
                    escaper = rightZoneEscapers[i];
                }
            }
        }
        return escaper;
    }
}