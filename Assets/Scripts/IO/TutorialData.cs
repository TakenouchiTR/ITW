using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.IO
{
    public struct TutorialData
    {
        public const int Version = 1;

        public string[] Titles { get; set; }
        public string[] Instructions { get; set; }
        public PartState[][] States { get; set; }
        public int StepCount => Titles.Length;
        public int PartCount => States.Length;
    }
}
