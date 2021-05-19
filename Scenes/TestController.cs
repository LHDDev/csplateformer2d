using Godot;
using System;

public class TestController : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Input.Singleton.Connect("joy_connection_changed", this, nameof(joy_con_changed));
    }

    private void joy_con_changed(int deviceId, bool isDeviceConnected)
    {
        GD.Print($"device {deviceId} - {(isDeviceConnected?"connected":"not connected")}");
    }
}
