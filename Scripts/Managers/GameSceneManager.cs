using BIS.Players;
using Unity.Cinemachine;
using UnityEngine;
namespace BIS
{
    public class GameSceneManager
    {
        public Player Player { get; private set; }
        public CinemachineCamera MainCamera { get; private set; }

        public void SetPlayer(Player player)
        {
            Player = player;
        }

        public void SetMainCamera(CinemachineCamera cam)
        {
            MainCamera = cam;
        }
    }
}
