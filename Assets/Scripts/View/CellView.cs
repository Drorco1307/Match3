using UnityEngine;

public class CellView : MonoBehaviour
{
    public bool IsAnimating = false;
    

    private SpriteRenderer _spriteComp;


    // Start is called before the first frame update
    public void Init()
    {
        _spriteComp = GetComponent<SpriteRenderer>();
    }

    public void SetType(int cellType)
    {
        switch (cellType)
        {
            case 0:
                _spriteComp.color = Color.blue;
                break;
            case 1:
                _spriteComp.color = Color.red;
                break;
            case 2:
                _spriteComp.color = Color.green;
                break;
            case 3:
                _spriteComp.color = Color.yellow;
                break;
            default:
                _spriteComp.color = Color.white;
                break;
        }

    }

    public void PlayExplosion()
    {
        IsAnimating = true;
        float itweenTime = 1.5f;
        transform.localScale = Vector3.one;
        iTween.ScaleTo(gameObject, new Vector3(5, 5, 5), itweenTime);
        iTween.ColorTo(gameObject, new Color(0,0,0,0), itweenTime);
        Invoke("EndAnimation", itweenTime + .1f);
    }

    private void EndAnimation()
    {
        IsAnimating = false;
    }
}
