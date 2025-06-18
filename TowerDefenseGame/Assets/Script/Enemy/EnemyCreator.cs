using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    GameObject enemy1_prefab;
    GameObject enemy2_prefab;
    float timer, maxTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy1_prefab = Resources.Load("enemy/Enemy_Kuma") as GameObject;
        enemy2_prefab = Resources.Load("enemy/Enemy_Kuma_2") as GameObject;
        SetRandomTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.PlayerInstance == null) { return; }
        timer += Time.deltaTime;
        if (timer > maxTimer)
        {
            timer = 0;
            float x = 9.0f;
            float y = Random.Range(-47, 47) * 0.1f;
            int r = Random.Range(0, 2);
            if(r == 0)
            {
                Enemy2Appear(new Vector3(x, y, 0));
            }
            else
            {
                Enemy1Appear(new Vector3(x, y, 0));
            }
            SetRandomTimer();
        }
    }
    public void Enemy1Appear(Vector3 pos)
    {
        GameObject g = Instantiate(enemy1_prefab);
        g.transform.position = pos;
    }
    public void Enemy2Appear(Vector3 pos)
    {
        GameObject g = Instantiate(enemy2_prefab);
        g.transform.position = pos;
    }
    public void SetRandomTimer()
    {
        maxTimer = Random.Range(1, 41) * 0.1f;
    }
}
