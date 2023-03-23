using System;
using UnityEngine;

namespace DI.MonoDI
{
    public class TestClass : MonoBehaviour
    {
        public Guid tt = new Guid();

        public TestClass() {
            tt = Guid.NewGuid();
        }

    }
}
