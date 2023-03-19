using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.MainProject.Scripts
{
    internal class MainViewModel : MonoBehaviour
    {
        public bool isEnable = true;
        public bool isCan = false;

        private ITestInterface govno;
        private int count = 0;

        private void Awake()
        {
            
        }

        private void Update()
        {
            //Debug.Log("isCan: " + isCan);
        }
        //public MainViewModel(){
        //    var str = "ds";
        //}

        public void ff()
        {
            count++;
        }
    }
}
