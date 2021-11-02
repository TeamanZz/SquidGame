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

    [HideInInspector] public List<WardenBase> wardens = new List<WardenBase>();
    [HideInInspector] public List<SniperWarden> sniperWardens = new List<SniperWarden>();
    public List<EscaperBase> leftZoneEscapers = new List<EscaperBase>();
    public List<EscaperBase> rightZoneEscapers = new List<EscaperBase>();

    private EscapersSpawner escapersSpawner;

    private void Awake()
    {
        Instance = this;
        escapersSpawner = GetComponent<EscapersSpawner>();
        moneyText.text = moneyCount.ToString();
    }

    public void UpdateMoneyCount()
    {
        moneyCount++;
        moneyText.text = moneyCount.ToString();
    }

    public void SetMoneyCount(int value)
    {
        moneyCount = value;
        moneyText.text = moneyCount.ToString();
    }

    public WardenBase GetRandomWarden(GameZone gameZone)
    {
        List<WardenBase> tempArray = new List<WardenBase>();
        for (int i = 0; i < wardens.Count; i++)
        {
            if (wardens[i].gameZone == gameZone)
                tempArray.Add(wardens[i]);
        }

        if (tempArray.Count != 0)
        {
            int wardenIndex = Random.Range(0, tempArray.Count);
            return tempArray[wardenIndex];
        }
        else
            return null;
    }

    public void AddWardenToList(WardenBase warden)
    {
        wardens.Add(warden);
    }

    public void AddWardenToList(SniperWarden sniperWarden)
    {
        sniperWardens.Add(sniperWarden);
    }

    public void RemoveWardenFromList(WardenBase warden)
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

    public void RemoveEscaperFromList(EscaperBase escaper, GameZone gameZone)
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

    public void AddEscaperToList(EscaperBase escaper, GameZone gameZone)
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

        for (int i = 0; i < sniperWardens.Count; i++)
        {
            sniperWardens[i].SetTarget();
        }
    }

    public List<EscaperBase> GetEscapersList(GameZone gameZone)
    {
        if (gameZone == GameZone.Left)
            return leftZoneEscapers;
        else
            return rightZoneEscapers;
    }

    public EscaperBase GetNearestToGateEscaper(GameZone gameZone)
    {
        float escaperZPos = 0;
        var distance = Mathf.Infinity;
        EscaperBase escaper = null;

        if (SpellsHandler.Instance.wallRemoved)
        {
            float smallestLeftDistance = Mathf.Infinity;
            float smallestRightDistance = Mathf.Infinity;

            EscaperBase leftEscaper = null;
            EscaperBase rightEscaper = null;

            for (int i = 0; i < leftZoneEscapers.Count; i++)
            {
                escaperZPos = leftZoneEscapers[i].transform.position.z;
                var newDist = Mathf.Abs(escaperZPos - leftEndGate.position.z);
                if (newDist < smallestLeftDistance)
                {
                    smallestLeftDistance = newDist;
                    leftEscaper = leftZoneEscapers[i];
                }
            }

            for (int i = 0; i < rightZoneEscapers.Count; i++)
            {
                escaperZPos = rightZoneEscapers[i].transform.position.z;
                var newDist = Mathf.Abs(escaperZPos - rightEndGate.position.z);
                if (newDist < smallestRightDistance)
                {
                    smallestRightDistance = newDist;
                    rightEscaper = rightZoneEscapers[i];
                }
            }

            if (smallestLeftDistance >= smallestRightDistance)
                return rightEscaper;
            else
                return leftEscaper;
        }

        if (gameZone == GameZone.Left)
        {
            for (int i = 0; i < leftZoneEscapers.Count; i++)
            {
                escaperZPos = leftZoneEscapers[i].transform.position.z;
                var newDist = Mathf.Abs(escaperZPos - leftEndGate.position.z);
                if (newDist < distance)
                {
                    distance = newDist;
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
                    distance = newDist;
                    escaper = rightZoneEscapers[i];
                }
            }
        }
        return escaper;
    }
}