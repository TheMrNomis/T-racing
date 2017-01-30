using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;

[System.Serializable]
public class SaveData : ISerializable
{
    //Attributes
    public VehicleStats stats;
    public HexMapEditor map;

    public SaveData()
    {
    }

    public SaveData(SerializationInfo info, StreamingContext ctxt)
    {
        //Get the values from info and assign them to the appropriate properties
        stats = (VehicleStats)info.GetValue("vehicleStat", typeof(VehicleStats));
        map = (HexMapEditor)info.GetValue("map", typeof(HexMapEditor));
    }


    //Serialization function.
    public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
    {
        info.AddValue("vehicleStat", (stats));
        info.AddValue("map", map);
    }
}
