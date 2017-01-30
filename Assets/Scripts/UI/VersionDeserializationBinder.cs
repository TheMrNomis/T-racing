﻿using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;
using System.Reflection;
using System;

public sealed class VersionDeserializationBinder : SerializationBinder
{

    public override Type BindToType(string assemblyName, string typeName)
    {
        if (!string.IsNullOrEmpty(assemblyName) && !string.IsNullOrEmpty(typeName))
        {
            Type typeToDeserialize = null;

            assemblyName = Assembly.GetExecutingAssembly().FullName;
            // The following line of code returns the type. 
            typeToDeserialize = Type.GetType(string.Format("{0}, {1}", typeName, assemblyName));
            return typeToDeserialize;
        }
        return null;
    }
}
