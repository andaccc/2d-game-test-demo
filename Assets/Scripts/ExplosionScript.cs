using UnityEngine;
using System.Collections;

namespace My
{
    public class ExplosionScript : MonoBehaviour
    {


        public void DeleteExplosion()
        {
            Debug.Log(this.GetType() + ", explosion deleted");
            Destroy(gameObject);
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}