using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Factory = FactoryController;

public class GameController : MonoBehaviour
{
    public GameObject BulletPrefab;

    private void Awake()
    {
        Factory.Clear();
        Factory.Register("bullet", BulletPrefab, 20);
    }
}
