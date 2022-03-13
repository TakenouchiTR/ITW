﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.IO
{
    public class TutorialWriter
    {
        public static void WriteFile(string fileLocation, TutorialData data)
        {
            using (BinaryWriter writer = new BinaryWriter(File.OpenWrite(fileLocation)))
            {
                writer.Write(TutorialData.Version);
                writer.Write(data.StepCount);
                writer.Write(data.PartCount);

                for (int i = 0; i < data.StepCount; i++)
                {
                    writer.Write(data.Titles[i]);
                    writer.Write(data.Instructions[i]);
                }

                for (int partIndex = 0; partIndex < data.PartCount; partIndex++)
                {
                    for (int stepIndex = 0; stepIndex < data.StepCount; stepIndex++)
                    {
                        Vector3 position = data.States[partIndex][stepIndex].Position;
                        writer.Write(position.x);
                        writer.Write(position.y);
                        writer.Write(position.z);
                    }
                }
            }
        }
    }
}
