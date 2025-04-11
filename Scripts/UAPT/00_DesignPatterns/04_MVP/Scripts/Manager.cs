using UnityEngine;

namespace BIS.Managers
{
    public class Manager : MonoBehaviour
    {
        private static Manager s_inctance;
        private static Manager Instacne
        {
            get
            {
                Init();
                return s_inctance;
            }
        }

        public static GameObject GO { get; set; }

        //private ResourceManager _resource = new ResourceManager();
        private RewindManager _rewind = new RewindManager();
        private GlobalVolumeManager _volume = new GlobalVolumeManager();
        private GameManager _game = new GameManager();
        private GameSceneManager _gameScene = new GameSceneManager();
        private CameraManager _camera = new CameraManager();

        //public static ResourceManager Resource { get { return Instacne._resource; } }
        public static RewindManager Rewind { get { return Instacne._rewind; } }
        public static GlobalVolumeManager Volume { get { return Instacne._volume; } }
        public static GameManager Game { get { return Instacne._game; } }
        public static GameSceneManager GameScene { get { return Instacne._gameScene; } }
        public static CameraManager Camera { get { return Instacne._camera; } }

        private static void Init()
        {
            if (s_inctance == null)
            {
                GO = GameObject.Find("@Manager");
                if (GO == null)
                {
                    GO = new GameObject { name = "@Manager" };
                    GO.AddComponent<Manager>();
                }

                DontDestroyOnLoad(GO);

                //√ ±‚»≠
                s_inctance = GO.GetComponent<Manager>();
            }
        }
    }

}
