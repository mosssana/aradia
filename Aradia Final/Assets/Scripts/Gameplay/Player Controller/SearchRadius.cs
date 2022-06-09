using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Searches for enemies near the player
/// </summary>
public class SearchRadius : MonoBehaviour
{
    public bool EnemySpotted
    {
        get { return enemiesList.Count > 0; }
    }

    List<GameObject> enemiesList = new List<GameObject>();

    public List<GameObject> EnemiesList
    {
        get
        {
            enemiesList.RemoveAll(item => item == null);
            return enemiesList;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        enemiesList.RemoveAll(item => item == null);
        
        if (col.gameObject.tag == "Enemy")
        {
            enemiesList.Add(col.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            enemiesList.Remove(col.gameObject);
        }
    }
}
