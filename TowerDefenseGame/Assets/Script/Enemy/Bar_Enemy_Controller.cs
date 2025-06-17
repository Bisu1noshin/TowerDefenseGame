using UnityEngine;

public class Bar_Enemy_Controller : MonoBehaviour
{
    RectTransform rect;
    PowerBar bar;
    RectTransform childT;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rect = GetComponent<RectTransform>();
        bar = GetComponentInChildren<PowerBar>();
        childT = GetComponentInChildren<RectTransform>();
    }

    // Update is called once per frame
    public void SetPosition(Vector2 pos)
    {
        rect.anchoredPosition = new Vector2(pos.x * 50, pos.y * 50 + 50);
        childT.anchoredPosition = new Vector2(pos.x * 50, pos.y * 50 + 50);
    }
    public void FillBar(float per)
    {
        bar.SetFillAmount(Mathf.Clamp01(per));
    }
    public void CrushBar()
    {
        Destroy(gameObject);
    }
}
