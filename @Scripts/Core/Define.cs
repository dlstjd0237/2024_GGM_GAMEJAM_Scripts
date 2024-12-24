using UnityEngine;
namespace BIS.Core
{
    public static class Define
    {
        public const float RecoredTime = 3.0f;

        public enum EInputType
        {
            PLAYER,
            UI,
            ALL
        }

        public enum ESceneType
        {
            Title,
            GameMenu,
            InGame,
        }

        public static class MLayerMask
        {
            public static readonly LayerMask WhatIsGround = LayerMask.GetMask("Ground");
            public static readonly LayerMask WhatIsPlayer = LayerMask.GetMask("Player");
            public static readonly LayerMask WhatIsEnemy = LayerMask.GetMask("Enemy");
        }

        public enum EUIEventType
        {
            DOWN,
            MOVE,
            ENTER,
            EXIT,
            CLICK
        }

        public enum EObjectTag
        {
            Player,
            Enemy
        }
    }


}
