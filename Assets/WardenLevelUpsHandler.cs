using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardenLevelUpsHandler : MonoBehaviour
{
    public static WardenLevelUpsHandler Instance;

    public GameObject mergeParticles;

    public List<WardenRifleLvlUpInfo> rifleLvlUps = new List<WardenRifleLvlUpInfo>();
    public List<WardenBazookaLvlUpInfo> bazookaLvlUps = new List<WardenBazookaLvlUpInfo>();
    public List<WardenShotgunLvlUpInfo> shotgunLvlUps = new List<WardenShotgunLvlUpInfo>();

    private void Awake()
    {
        Instance = this;
    }

    public void LvlUpWarden(WardenRifle warden)
    {
        WardenLevelUpsHandler levelUpsHandler = GetComponent<WardenLevelUpsHandler>();
        Instantiate(mergeParticles, new Vector3(warden.transform.position.x, warden.transform.position.y - 0.1f, warden.transform.position.z), Quaternion.identity);

        List<WardenRifleLvlUpInfo> info = new List<WardenRifleLvlUpInfo>();
        info = levelUpsHandler.rifleLvlUps;

        float newScale = info[warden.wardenStrenght].newScale;
        warden.transform.localScale = new Vector3(newScale, newScale, newScale);

        warden.timeBetweenShots = info[warden.wardenStrenght].newTimeBetweenAttacks;
        warden.damage = info[warden.wardenStrenght].newDamage;
    }

    public void LvlUpWarden(WardenBazooka warden)
    {
        WardenLevelUpsHandler levelUpsHandler = GetComponent<WardenLevelUpsHandler>();
        Instantiate(mergeParticles, new Vector3(warden.transform.position.x, warden.transform.position.y - 0.1f, warden.transform.position.z), Quaternion.identity);

        List<WardenBazookaLvlUpInfo> info = new List<WardenBazookaLvlUpInfo>();
        info = levelUpsHandler.bazookaLvlUps;

        float newScale = info[warden.wardenStrenght].newScale;
        warden.transform.localScale = new Vector3(newScale, newScale, newScale);

        warden.timeBetweenShots = info[warden.wardenStrenght].newTimeBetweenAttacks;
        warden.damage = info[warden.wardenStrenght].newDamage;
    }

    public void LvlUpWarden(WardenShotgun warden)
    {
        WardenLevelUpsHandler levelUpsHandler = GetComponent<WardenLevelUpsHandler>();
        Instantiate(mergeParticles, new Vector3(warden.transform.position.x, warden.transform.position.y - 0.1f, warden.transform.position.z), Quaternion.identity);

        List<WardenShotgunLvlUpInfo> info = new List<WardenShotgunLvlUpInfo>();
        info = levelUpsHandler.shotgunLvlUps;

        float newScale = info[warden.wardenStrenght].newScale;
        warden.transform.localScale = new Vector3(newScale, newScale, newScale);

        warden.timeBetweenShots = info[warden.wardenStrenght].newTimeBetweenAttacks;
        warden.damage = info[warden.wardenStrenght].newDamage;

    }
}

[System.Serializable]
public class WardenBaseLvlUpInfo
{
    public float newScale;
    public float newTimeBetweenAttacks;
}

[System.Serializable]
public class WardenRifleLvlUpInfo : WardenBaseLvlUpInfo
{
    public float newDamage;
}

[System.Serializable]
public class WardenBazookaLvlUpInfo : WardenBaseLvlUpInfo
{
    public float newDamage;
}

[System.Serializable]
public class WardenShotgunLvlUpInfo : WardenBaseLvlUpInfo
{
    public float newDamage;
}