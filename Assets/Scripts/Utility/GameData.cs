using UnityEngine;

/// <summary>
/// Save here game data information.
/// </summary>
public class GameData : DontDestroyOnLoadObject<GameData>
{
    // How much races was complet.
    public int completedRaces;

    // Muted or not Background Sounds.
    public bool backgroundSoundIsMuted = false;
    // Muted or not Game Sounds.
    public bool gameSoundIsMuted = false;

}
