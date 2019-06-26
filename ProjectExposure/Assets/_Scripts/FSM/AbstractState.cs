using System;
using System.Diagnostics;
using UnityEngine;


    public abstract class AbstractState<T> :MonoBehaviour where T : class,IAgent
    {
        [HideInInspector]
        public T target;

        public virtual void Enter(IAgent pAgent)
        {
           
            enabled = true;
        }
      
        public virtual void Exit(IAgent pAgent)
        {
            enabled = false;
        }

    }


