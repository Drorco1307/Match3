using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellView : MonoBehaviour
{

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
}
