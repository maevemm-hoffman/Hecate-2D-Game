using UnityEngine;

public class zombieManager : MonoBehaviour
{
    public float highSpeed = 7f;
    public float lowSpeed = 1f;

    private enemyManager enemy;

    void Start()
    {
        enemy = GetComponent<enemyManager>();
    }

    public void changeSpeed(float health)
    {
        if (health > 2) { enemy.speed = Map(health, 2, enemy.maxHealth, lowSpeed, enemy.speed); }
        else
        {
            enemy.speed = highSpeed;
            enemy.animation.SetBool("isAgro", true); }
    }

    private static float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
    }
}