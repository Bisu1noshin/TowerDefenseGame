using UnityEngine;

public class Bar_Enemy_Controller : MonoBehaviour
{
    RectTransform rect;
    PowerBar bar;
    [SerializeField] float per;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rect = GetComponent<RectTransform>();
        bar = GetComponentInChildren<PowerBar>();
    }

    // Update is called once per frame
    public void SetPosition(Vector2 pos)
    {
        rect.anchoredPosition = pos * 10 + new Vector2(550.0f, 250.0f);
    }
    public void FillBar(float per)
    {
        this.per = per;
        bar.SetFillAmount(per);
    }
}
