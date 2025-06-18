using UnityEngine;
using UnityEngine.UI;

public class Bar_Enemy_Controller : MonoBehaviour
{
    RectTransform rect;
    PowerBar bar;
    RectTransform childT;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        bar = GetComponentInChildren<PowerBar>();
        childT = GetComponentInChildren<RectTransform>();
        rect.anchoredPosition = new Vector2(0, -1000);
        childT.anchoredPosition = new Vector2(0, -1000);
    }

    // Update is called once per frame
    public void SetPosition(Vector2 pos)
    {
        Vector2 vec = new Vector2(pos.x * 70, pos.y * 80 + 50);
        rect.anchoredPosition = vec;
        childT.anchoredPosition = vec;
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
