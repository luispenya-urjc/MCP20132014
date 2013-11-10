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

	}
}
