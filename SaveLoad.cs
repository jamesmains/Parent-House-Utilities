using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Todo -- REPLACE BINARY FORMATTER
namespace ParentHouse.Utilities {
    public static class SaveLoad {
        /// <summary>
        /// path conventions
        /// n = name, d = day, w = week
        /// </summary>
        public static string DebugFilePath => Application.persistentDataPath + "/debug.level";

        public static string DummyFilePath => Application.persistentDataPath + "/debug.level";
        public static string LevelsFilePath => Application.streamingAssetsPath + "/Levels/";

        public static string LevelFilePath(string n) {
            return Application.streamingAssetsPath + $"/Levels/{n}.level";
        }

        public static void Save(SaveData saveData, string filePath) {
            //Debug.Log("Mission Start");
            try {
                if (!Directory.Exists(Path.GetDirectoryName(filePath))) {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
                    Debug.Log(
                        $"Path's directory did not exist. Creating directory at {Path.GetDirectoryName(filePath)}");
                }

                BinaryFormatter formatter = new();
                var stream = new FileStream(filePath, FileMode.Create);
                formatter.Serialize(stream, saveData);
                stream.Close();
                //Debug.Log("Mission Complete! Great job Star Fox!");
            }
            catch (Exception e) {
                Debug.LogError($"Trying to save file that isn't savedata or can't exist at: {filePath} because {e}");
            }
        }

        public static SaveData Load(string filePath) {
            //Debug.Log("Mission Start");
            try {
                BinaryFormatter formatter = new();
                var stream = new FileStream(filePath, FileMode.Open);
                var data = formatter.Deserialize(stream) as SaveData;
                stream.Close();
                //Debug.Log("Mission Complete! Great job Star Fox!");
                return data;
            }
            catch (Exception e) {
                Debug.LogError($"Trying to load file that isn't savedata or doesn't exist at: {filePath} because {e}");
                return null;
            }
        }

        public static void DeleteSaveData(string filePath) {
            try {
                BinaryFormatter formatter = new();
                var stream = new FileStream(filePath, FileMode.Open);
                var data = formatter.Deserialize(stream) as SaveData;
                stream.Close();
                File.Delete(filePath);
            }
            catch (Exception e) {
                Debug.LogError($"Trying to delete file that isn't savedata or doesn't exist at: {filePath}");
            }
        }

        public static bool HasPath(string path) {
            return File.Exists(path);
        }
    }

    [Serializable]
    public abstract class SaveData {
    }
}
