using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSettings : MonoBehaviour
{
    public static LevelSettings Instance;

    public bool overridePrefs;
    public int lastLevelIndex = 0;

    public List<LevelSetup> levelSetups = new List<LevelSetup>();
    [HideInInspector] public LevelSetup currentSetup;

    [Header("Setup Elements")]
    public List<GameObject> wardensButtons = new List<GameObject>();
    public List<GameObject> SpellsButtons = new List<GameObject>();

    public List<GameObject> allEscapersList = new List<GameObject>();

    [HideInInspector] public List<GameObject> availableEscapersList = new List<GameObject>();

    public RectTransform skillsPanel;

    private void Awake()
    {
        Instance = this;
        if (!overridePrefs)
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

        skillsPanel.sizeDelta = new Vector2(skillsPanel.sizeDelta.x, currentSetup.spellsPanelHeight);

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

        escapersSpawner.minSpawnTime = currentSetup.minSpawnTime;
        escapersSpawner.maxSpawnTime = currentSetup.maxSpawnTime;

        escapersSpawner.minCrowdCount = currentSetup.minCrowdCount;
        escapersSpawner.maxCrowdCount = currentSetup.maxCrowdCount;

        escapersSpawner.minSpawnTimeDecrease = currentSetup.minSpawnTimeDecrease;
        escapersSpawner.maxSpawnTimeDecrease = currentSetup.maxSpawnTimeDecrease;
    }
}

[System.Serializable]
public class LevelSetup
{
    public int startRemainingEscapers;
    public int startMoneyCount;
    [Space]

    public int unusualEnemySpawnChance;
    public int crowdSpawnChance;
    [Space]

    public float minSpawnTime = 1;
    public float maxSpawnTime = 5;
    [Space]

    public int minCrowdCount = 1;
    public int maxCrowdCount = 3;
    [Space]
    public float minSpawnTimeDecrease;
    public float maxSpawnTimeDecrease;

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

    [Header("Spells (335,235,145)")]
    public float spellsPanelHeight;
    [Space]
    public bool spikeSpellEnabled;
    public bool obstacleSpellEnabled;
    public bool wallSpellEnabled;
}