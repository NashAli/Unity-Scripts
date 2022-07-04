/*
 * Graffiti Softwerks 2022
 * PoolManager.cs
 * Author: Nash Ali
 * Creation Date: 05-13-2022
 * Last Update: 05-14-2022
 * 
 * Copyright (c) Graffiti Softwerks 2022
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> bulletPool, bombPool, missilePool, enemyPool, enemyShipPool;
    [SerializeField] private GameObject _bulletPrefab, _bombPrefab, _missilePrefab, _enemyPrefab, _enemyShipPrefab;
    [SerializeField] private GameObject _bulletContainer, _bombContainer, _missileContainer, _enemyContainer, _enemyShipContainer;
    [SerializeField] private int _bullet, _bomb, _missile, _enemy, _ship;

    private static PoolManager _instance;

    public static PoolManager Instance
    {

        get
            {
            if (_instance == null)
            {
                Debug.LogError("Pool Manager is null");
            }
            return _instance; }

    }
    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        bulletPool = GenerateBullets(_bullet);
        bombPool = GenerateBombs(_bomb);
        missilePool = GenerateMissiles(_missile);
        enemyPool = GenerateEnemies(_enemy);
    }

    private List<GameObject> GenerateMissiles( int numOfMissiles)
    {
        for (int i = 0; i < numOfMissiles; i++)
        {
            GameObject missile = Instantiate(_missilePrefab);
            missile.transform.parent = _missileContainer.transform;
            missile.SetActive(false);
            missilePool.Add(missile);
        }
        return enemyPool;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="numOfEnemies"></param>
    /// <returns></returns>
    private List<GameObject> GenerateEnemies ( int numOfEnemies)
    {
        for (int i = 0; i < numOfEnemies; i++)
        {
            GameObject enemy = Instantiate(_enemyPrefab);
            enemy.transform.parent = _enemyContainer.transform;
            enemy.SetActive(false);
            enemyPool.Add(enemy);
        }
        return enemyPool;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="numOfBombs"></param>
    /// <returns></returns>
    private List<GameObject> GenerateBombs(int numOfBombs)
    {
        for (int i = 0; i < numOfBombs; i++)
        {
            GameObject bomb = Instantiate(_bombPrefab);
            bomb.transform.parent = _bombContainer.transform;
            bomb.SetActive(false);
            bombPool.Add(bomb);
        }
        return bombPool;
    }

    private List<GameObject> GenerateBullets(int numOfBullets)
    {
        for (int i = 0; i < numOfBullets; i++)
        {
            GameObject bullet = Instantiate(_bombPrefab);
            bullet.transform.parent = _bombContainer.transform;
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
        return bombPool;
    }


    public GameObject RequestBullet()
    {
        foreach (var bullet in bulletPool)
        {
            if (bullet.activeInHierarchy == false)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }
        GameObject newBullet = Instantiate(_bulletPrefab);
        newBullet.transform.parent = _bulletContainer.transform;
        bulletPool.Add(newBullet);
        return newBullet;
    }




    public GameObject RequestBomb()
    {
        foreach(var bomb in bombPool)
        {
            if(bomb.activeInHierarchy == false)
            {
                bomb.SetActive(true);
                return bomb;
            }
        }
        GameObject newBomb = Instantiate(_bombPrefab);
        newBomb.transform.parent = _bombContainer.transform;
        bombPool.Add(newBomb);
        return newBomb;
    }


    public GameObject RequestMissile()
    {
        foreach (var missile in missilePool)
        {
            if (missile.activeInHierarchy == false)
            {
                missile.SetActive(true);
                return missile;
            }
        }
        GameObject newBomb = Instantiate(_bombPrefab);
        newBomb.transform.parent = _bombContainer.transform;
        bombPool.Add(newBomb);
        return newBomb;
    }



    public GameObject RequestEnemy()
    {
        foreach(var target in enemyPool)
        {
            if(target.activeInHierarchy == false)
            {
                target.SetActive(true);
                return target;
            }
        }
        GameObject newEnemy = Instantiate(_enemyPrefab);
        newEnemy.transform.parent = _enemyContainer.transform;
        enemyPool.Add(newEnemy);
        return newEnemy;
    }

}
