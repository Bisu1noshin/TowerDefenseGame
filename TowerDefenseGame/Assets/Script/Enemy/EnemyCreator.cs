using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    GameObject enemy_prefab;
    float timer, maxTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy_prefab = Resources.Load("enemy/Enemy_Kuma") as GameObject;
        SetRandomTimer();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > maxTimer)
        {
            timer = 0;
            float x = 9.0f;
            float y = Random.Range(-47, 47) * 0.1f;
            EnemyAppear(new Vector3(x, y, 0));
            SetRandomTimer();
        }
    }
    public void EnemyAppear(Vector3 pos)
    {
        GameObject g = Instantiate(enemy_prefab);
        g.transform.position = pos;
    }
    public void SetRandomTimer()
    {
        maxTimer = Random.Range(1, 41) * 0.1f;
    }
}
