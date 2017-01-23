using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbox : Singleton<Toolbox> {
    protected Toolbox() { }
    void Awake() {
        RegisterComponent<MicrophoneInput>();
    }
    public static T RegisterComponent<T>() where T: Component {
        return Instance.GetOrAddComponent<T>();
    }
}
