using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPerception : MonoBehaviour
{
    public BGM BGM;
    public LayerMask myEnemy = default;
    public List<GameObject> myEnemylist = new List<GameObject>();
    public IBattle myTarget = null;
    public event MyAction FindTarget = null; // delegate
    public event MyAction LostTarget = null; // delegate
    // Start is called before the first frame update
    void Start()
    {
        BGM.BGMSpeeker.clip = BGM.BGMSound[0]; // Start BGM
        BGM.BGMSpeeker.Play();
    }

    public void OnLostTarget()
    {
        //Target lost
        myTarget = null;
        LostTarget?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((myEnemy & 1 << other.gameObject.layer) != 0)
        {
            myEnemylist.Add(other.gameObject);
            if (myTarget == null)
            {
                IBattle ib = other.transform.GetComponent<IBattle>();
                if (ib != null)
                {
                    if (ib.IsLive)
                    {
                        myTarget = other.transform.GetComponent<IBattle>();
                        FindTarget?.Invoke();
                        BGM.BGMSpeeker.clip = BGM.BGMSound[1];
                        BGM.BGMSpeeker.Play();
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (myTarget != null && other.transform == myTarget.transform)
        {
            OnLostTarget();
            BGM.BGMSpeeker.clip = BGM.BGMSound[0];
            BGM.BGMSpeeker.Play();         
        }
        myEnemylist.Remove(other.gameObject);
    }
}