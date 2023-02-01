using System;
using System.Collections;
using System.Collections.Generic;
using EndRun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EndRun
{

    public class ObstactionController : MonoBehaviour
    {
        [Header("Spawn position")]
        [SerializeField] private Transform start;
        [SerializeField] private Transform end;

        [Header("Respawn time")] 
        [SerializeField] [Range(2f, 10f)] private float spawnTime;

        [Header("Obstaction prefab")] [SerializeField]
        private GameObject obstactionObject;

        private List<GameObject> obstactionPool = new List<GameObject>();
        private List<GameObject> obstactionList = new List<GameObject>();

        private void OnEnable()
        {
            StartCoroutine(CreateMob());
        }

        private void OnDisable()
        {
            StopCoroutine(CreateMob());
        }

        IEnumerator CreateMob()
        {
            while (true)
            {
                GameObject newMob = PopMob();
                newMob.transform.position = start.position;
                newMob.SetActive(true);
                obstactionList.Add(newMob);

                yield return new WaitForSeconds(Random.Range(2f, spawnTime));
            }
        }

        void Update()
        {
            if( GameSettings.gameData.isEnd )
                return;
            
            foreach (GameObject mob in obstactionList)
            {
                mob.transform.Translate(Vector3.left * Time.deltaTime * GameSettings.gameData.moveSpeed);
            }

            if (obstactionList[0].transform.position.x < end.position.x)
            {
                GameObject deleteMob = obstactionList[0];
                obstactionList.Remove(deleteMob);
                PushMob(deleteMob);
            }
        }

        private GameObject PopMob()
        {
            GameObject newMob;
            if (obstactionPool.Count == 0)
            {
                newMob = Instantiate(obstactionObject, gameObject.transform);
                obstactionPool.Add(newMob);
            }
            
            newMob = obstactionPool[0];
            obstactionPool.Remove(newMob);
            return newMob;
        }

        private void PushMob(GameObject deleteMob)
        {
            deleteMob.transform.position = start.position;
            deleteMob.SetActive(false);
            obstactionPool.Add(deleteMob);
        }

        private void OnDestroy()
        {
            foreach (var obs in obstactionPool)
            {
                Destroy(obs);
            }
            obstactionPool.Clear();
            
            foreach (var obs in obstactionList)
            {
                Destroy(obs);
            }
            obstactionList.Clear();
        }

    }
}