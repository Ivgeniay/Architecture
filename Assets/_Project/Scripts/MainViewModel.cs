﻿using DI.MonoDI;
using UnityEngine;

namespace Assets.MainProject.Scripts
{
    internal class MainViewModel : MonoBehaviour
    {
        public bool isEnable = true;
        public bool isCan = false;

        private ITestInterface govno;
        private int count = 0;

        private void Awake() {
        }

        private void Update() {
        }

        TestClass testClass;
        private void Construct(TestClass testClass)
        {
            this.testClass = testClass;
            Debug.Log("From construct!");
        }

        public void ff() {
            count++;
        }
    }
}
