using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetBG : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Sprite> backgrounds = new List<Sprite>();

    private void Start()
    {
        GetComponent<Image>().sprite = PRUtils.GetSingle(backgrounds);
    }
}
