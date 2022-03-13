using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.IO
{
    public class TutorialReader
    {
        public static TutorialData ReadFile(string fileLocation)
        {
            TutorialData result = new TutorialData();
            using (BinaryReader reader = new BinaryReader(File.OpenRead(fileLocation)))
            {
                int version = reader.ReadInt32();
                switch (version)
                {
                    case 1:
                        return ReadVersion1(reader);
                }
            }

            return result;
        }

        private static TutorialData ReadVersion1(BinaryReader reader)
        {
            int stepCount = reader.ReadInt32();
            int partCount = reader.ReadInt32();

            string[] titles = new string[stepCount];
            string[] instructions = new string[stepCount];
            PartState[][] states = new PartState[partCount][];

            for (int stepIndex = 0; stepIndex < stepCount; stepIndex++)
            {
                titles[stepIndex] = reader.ReadString();
                instructions[stepIndex] = reader.ReadString();
            }

            for (int partIndex = 0; partIndex < partCount; partIndex++)
            {
                states[partIndex] = new PartState[stepCount];
                for (int stepIndex = 0; stepIndex < stepCount; stepIndex++)
                {
                    float x = reader.ReadSingle();
                    float y = reader.ReadSingle();
                    float z = reader.ReadSingle();
                    states[partIndex][stepIndex] = new PartState() { 
                        Position = new Vector3(x, y, z) 
                    };
                }
            }

            TutorialData data = new TutorialData()
            {
                Titles = titles,
                Instructions = instructions,
                States = states
            };

            return data;
        }
    }
}
