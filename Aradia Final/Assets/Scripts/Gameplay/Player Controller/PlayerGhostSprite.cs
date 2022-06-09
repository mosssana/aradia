using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhostSprite : MonoBehaviour
{
    SpriteRenderer sprite;
    float timer = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        //transform.position = StateControl.Instance.transform.position;
        //transform.localScale = StateControl.Instance.transform.localScale;
        //sprite.sprite = StateControl.Instance.SpriteRenderer.sprite;
        sprite.color = new Vector4(50, 50, 50, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        
        if(timer <= 0) {
            Destroy(gameObject);
        }
    }
}
