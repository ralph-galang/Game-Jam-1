using UnityEngine;

[System.Serializable]
public class PlayerData
{
    [SerializeField] int currentHP = 100;
    [SerializeField] float walkSpeed = 10f;
    [SerializeField] float rotateSpeed = 30f;

    public int getHP() { return currentHP; }
    public void setHP(int val) { currentHP = val; }
    public float getWalkSpd() { return walkSpeed; }
    public void setWalkSpd(int val) { walkSpeed = val; }
    public float getRotateSpd() {return rotateSpeed; }
    public void setRotateSpd(int val) { rotateSpeed = val; }
}
