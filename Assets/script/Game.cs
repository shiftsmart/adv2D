using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    public TextMeshProUGUI FPS;
    private float time1;
    private int framecount;
    private float pollingTime = 1f;


    //private void Update() {
    //    time1 += Time.deltaTime;
    //    framecount++;
    //    if (time1 >= pollingTime)
    //    {
    //        int frameRate = Mathf.RoundToInt(framecount / time1);
    //        FPS.text = framecount.ToString();
    //        time1 -= pollingTime;
    //        framecount = 0;
    //    }
    //}

    public SaveData sav = new SaveData();
    public bool pause = false;


    public Transform Player() {
        return GameObject.FindWithTag("player").transform;


    }





}


public class SaveData
{
    public float maxHP = 500;
    public float hp = 50f;
    public float maxSP = 500;
    public float sp = 500;
    public int money = 0;
    public float skillCost = 100;
    public float regSpeed = 12;
    public float ammoCost = 5;

    public void GainMoney(int i) {
        money = Mathf.Clamp(money + i, 0, 999);
    }


    public void CostSP(float s) {
        sp = Mathf.Clamp(sp - s, 0, maxSP);

    }


    public void RegenSP(float f) {
        CostSP(-regSpeed * f);
    }

    public void Damage(float dmg) {
        hp = Mathf.Clamp(hp - dmg, 0, maxHP);

    }



}