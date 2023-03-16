using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.MainProject.Scripts
{
    internal class MainViewModel
    {
        private IGovno govno;
        private int count = 0;
        public MainViewModel(IGovno govno) {
            this.govno = govno;

            
            var str = "ds";
        }

        public void ff()
        {
            count++;
        }
    }
}
