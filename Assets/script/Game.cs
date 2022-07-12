using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{


    public SaveData sav = new SaveData();
    public bool pause = false;
    public Transform Player(){
        return GameObject.FindWithTag("player").transform;


    }



}


public class SaveData
{
    float maxHP = 5;
    float hp = 1.5f;
    float maxSP = 500;
    float sp = 500;
    int money = 0;
    float skillCost = 100;
    float regSpeed = 12;
    float ammoCost = 5;

    public void GainMoney(int i) {
        money = Mathf.Clamp(money+i,0,999);
    }


    public void CostSP(float s) {
        sp = Mathf.Clamp(sp-s,0,maxSP);
    
    }


    public void RegenSP(float f) {
        CostSP(-regSpeed*f);
    }





}