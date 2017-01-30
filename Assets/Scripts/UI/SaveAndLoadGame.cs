using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveAndLoadGame : MonoBehaviour {

	// Function which save the game
	public void Save ()
    {
        SaveData data = new SaveData();
        Stream stream = File.Open("MySavedGame.game", FileMode.Create);
        BinaryFormatter bformatter = new BinaryFormatter();
        bformatter.Binder = new VersionDeserializationBinder();
        Debug.Log("Writing Information");
        bformatter.Serialize(stream, data);
        stream.Close();
    }

    // Function which load the saved game
    public void Load ()
    {
        SaveData data = new SaveData();
        Stream stream = File.Open("MySavedGame.gamed", FileMode.Open);
        BinaryFormatter bformatter = new BinaryFormatter();
        bformatter.Binder = new VersionDeserializationBinder();
        Debug.Log("Reading Data");
        data = (SaveData)bformatter.Deserialize(stream);
        stream.Close();
    }
}
