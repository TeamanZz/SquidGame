using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSettings : MonoBehaviour
{
    public static LevelSettings Instance;

    public int lastLevelIndex = 0;

    public List<LevelSetup> levelSetups = new List<LevelSetup>();
    [HideInInspector] public LevelSetup currentSetup;

    [Header("Setup Elements")]
    public List<GameObject> wardensButtons = new List<GameObject>();
    public List<GameObject> SpellsButtons = new List<GameObject>();

    public List<GameObject> allEscapersList = new List<GameObject>();

    [HideInInspector] public List<GameObject> availableEscapersList = new List<GameObject>();

    private void Awake()
    {
        Instance = this;

        PlayerPrefs.DeleteAll();

        lastLevelIndex = PlayerPrefs.GetInt("LastLevelIndex");

        if (lastLevelIndex > 4)
            lastLevelIndex = 4;
        currentSetup = levelSetups[lastLevelIndex];

        GetComponent<ZoneManager>().SetMoneyCount(currentSetup.startMoneyCount);

        EscapersSpawner escapersSpawner = GetComponent<EscapersSpawner>();
        escapersSpawner.SetEscapersRemaining(currentSetup.startRemainingEscapers);

        escapersSpawner.unusualEscaperSpawnChance = currentSetup.unusualEnemySpawnChance;
        escapersSpawner.crowdSpawnChance = currentSetup.crowdSpawnChance;

        wardensButtons[0].SetActive(currentSetup.canSpawnRifle);
        wardensButtons[1].SetActive(currentSetup.canSpawnShotgun);
        wardensButtons[2].SetActive(currentSetup.canSpawnBazooka);
        wardensButtons[3].SetActive(currentSetup.canSpawnSniper);

        SpellsButtons[0].SetActive(currentSetup.spikeSpellEnabled);
        SpellsButtons[1].SetActive(currentSetup.obstacleSpellEnabled);
        SpellsButtons[2].SetActive(currentSetup.wallSpellEnabled);

        if (currentSetup.canSpawnEscaper)
            availableEscapersList.Add(allEscapersList[0]);

        if (currentSetup.canSpawnHeavyEscaper)
            availableEscapersList.Add(allEscapersList[1]);

        if (currentSetup.canSpawnFastEscaper)
            availableEscapersList.Add(allEscapersList[2]);

        if (currentSetup.canSpawnKnifeEscaper)
            availableEscapersList.Add(allEscapersList[3]);

        escapersSpawner.escapers = availableEscapersList;
    }
}

[System.Serializable]
public class LevelSetup
{
    public int startRemainingEscapers;
    public int startMoneyCount;

    public int unusualEnemySpawnChance;
    public int crowdSpawnChance;

    [Header("Wardens")]
    public bool canSpawnRifle = true;
    public bool canSpawnShotgun;
    public bool canSpawnSniper;
    public bool canSpawnBazooka;

    [Header("Escapers")]
    public bool canSpawnEscaper;
    public bool canSpawnHeavyEscaper;
    public bool canSpawnFastEscaper;
    public bool canSpawnKnifeEscaper;

    [Header("Spells")]
    public bool spikeSpellEnabled;
    public bool obstacleSpellEnabled;
    public bool wallSpellEnabled;
}