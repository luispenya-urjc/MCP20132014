using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FSM
{
	public class Environment: MonoBehaviour
	{
        private bool sound=false;

        public bool Sound
        {
            get { return sound; }
            set { sound = value; }
        }

        private int wounds=0;

        public int Wounds
        {
            get { return wounds; }
            set { wounds = value; }
        }
        private bool alive=true;

        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }
        private bool show=false;

        public bool Show
        {
            get { return show; }
            set { show = value; }
        }

        public override string ToString()
        {
            string st= base.ToString()+"\n";
            st += "Alive : " + alive  + "\n";
            st += "Show  : " + show   + "\n";
            st += "Sound : " + sound  + "\n";
            st += "Wounds: " + wounds + "\n";
            return st;
        }

        void Start()
        {

            for (int x = 0; x < 5; x++)
            {
                GameObject[] s = GameObject.FindGameObjectsWithTag("spawnarea-attack");
                int i = UnityEngine.Random.Range(0, s.Length);
                Vector3 pos = s[i].transform.position + (UnityEngine.Random.insideUnitSphere * (s[i].transform.localScale.x / 2.0f));
                pos.y = 1f;
                Instantiate(Resources.Load("FSMActor"), pos, Quaternion.identity).name = "nuevo"+x;

                Debug.Log("Position of cube: " + pos);
            }

            for (int x=0; x<3; x++){

            }
        }
	}
}
