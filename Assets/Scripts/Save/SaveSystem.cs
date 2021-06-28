using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
   public static void SaveGame(SaveManager save)
   {
      BinaryFormatter formatter = new BinaryFormatter();
      string path = Application.persistentDataPath + "/save.save";
      FileStream stream = new FileStream(path, FileMode.Create);
      
      SaveData data = new SaveData(save);
      
      formatter.Serialize(stream, data);
      stream.Close();
      
      Debug.Log("Game saved");
   }

   public static SaveData LoadGame()
   {
      string path = Application.persistentDataPath + "/save.save";
      if (File.Exists(path))
      {
         BinaryFormatter formatter = new BinaryFormatter();
         FileStream stream = new FileStream(path, FileMode.Open);

         SaveData data = formatter.Deserialize(stream) as SaveData;
         stream.Close();

         return data;
      }
      else
      {
         Debug.LogError("Save file not found in" + path);
         return null;
      }
   }
}
