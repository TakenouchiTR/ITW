using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.IO
{
    /// <summary>
    ///     Writes tutorial data to a specified file.
    /// </summary>
    public class TutorialWriter
    {
        /// <summary>
        ///     Writes a tutorial file at a specified location. The version of the file is determined by <br />
        ///     <see cref="TutorialData"/>'s <c>Version</c> constant field.
        /// </summary>
        /// <param name="fileLocation">The file location.</param>
        /// <param name="data">The tutorial data.</param>
        public static void WriteFile(string fileLocation, TutorialData data)
        {
            using BinaryWriter writer = new BinaryWriter(File.OpenWrite(fileLocation));

            writer.Write(TutorialData.Version);
            writer.Write(data.StepCount);
            writer.Write(data.PartCount);

            for (int i = 0; i < data.StepCount; i++)
            {
                writer.Write(data.StepInformation[i].Title);
                writer.Write(data.StepInformation[i].Subtitle);
                writer.Write(data.StepInformation[i].Instructions);
            }

            for (int partIndex = 0; partIndex < data.PartCount; partIndex++)
            {
                writer.Write(data.States[partIndex].PartName);
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
